﻿using Microsoft.Extensions.Logging;
using TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Comum;
using TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Lancar.DTOs;
using TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Lancar.interfaces;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Comum;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Enums;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Eventos;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Interfaces;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Mapeadores;
using TorneSe.ServicoLancamentoNotas.Dominio.Clients;
using TorneSe.ServicoLancamentoNotas.Dominio.Repositories;

namespace TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Lancar;

public sealed class LancarNota : NotaHandler, ILancarNota
{
    private readonly INotaRepository _notaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LancarNota> _logger;
    private readonly IMediatorHandler _mediatorHandler;
    public LancarNota(INotaRepository notaRepository,
                      IUnitOfWork unitOfWork,
                      ILogger<LancarNota> logger, ICursoClient cursoClient,
                      IMediatorHandler mediatorHandler) : base(cursoClient)
    {
        _notaRepository = notaRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<Resultado<NotaOutputModel>> Handle(LancarNotaInput request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.NotaSubstitutiva)
                await TentarCancelarNota(request,cancellationToken);

            var novaNota = MapeadorAplicacao.LancarNotaInputEmNota(request);

            if (!novaNota.EhValida)
                return Resultado<NotaOutputModel>.RetornaResultadoErro(TipoErro.NotaInvalida,
                        novaNota.Notificacoes.Select(notificacao => new DetalheErro(notificacao.Campo, notificacao.Mensagem)).ToList());

            var (valido, detalhes) = await ValidarInformacoesAlunoCurso(
                new ValidacaoCursoInput(request.AlunoId, request.ProfessorId, request.AtividadeId), cancellationToken);

            if (!valido && detalhes.Any())
                return Resultado<NotaOutputModel>.RetornaResultadoErro(TipoErro.NaoFoiPossivelValidarVinculosCursos, detalhes);

            await _notaRepository.Inserir(novaNota, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            await _mediatorHandler.PublicarEvento(new NotaLancadaEvento(novaNota.Id), cancellationToken);

            return Resultado<NotaOutputModel>.RetornaResultadoSucesso(MapeadorAplicacao.NotaEmNotaOutputModel(novaNota));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Resultado<NotaOutputModel>.RetornaResultadoErro(TipoErro.ErroInesperado);
        }
    }

    private async Task TentarCancelarNota(LancarNotaInput request, CancellationToken cancellationToken)
    {
        var nota = await _notaRepository.BuscarNotaPorAlunoEAtividade(request.AlunoId, request.AtividadeId, cancellationToken);
        if (nota is not null)
        {
            nota.CancelarPorRetentativa();
            await _notaRepository.Atualizar(nota, cancellationToken);
        }
    }
}
