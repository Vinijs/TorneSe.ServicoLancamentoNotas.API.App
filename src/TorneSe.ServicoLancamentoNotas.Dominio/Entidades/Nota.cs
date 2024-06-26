﻿using TorneSe.ServicoLancamentoNotas.Dominio.Constantes;
using TorneSe.ServicoLancamentoNotas.Dominio.Enums;
using TorneSe.ServicoLancamentoNotas.Dominio.Params;
using TorneSe.ServicoLancamentoNotas.Dominio.SeedWork;
using TorneSe.ServicoLancamentoNotas.Dominio.Validacoes;
using TorneSe.ServicoLancamentoNotas.Dominio.Validacoes.Validador;

namespace TorneSe.ServicoLancamentoNotas.Dominio.Entidades;

public partial class Nota : Entidade, IRaizAgregacao
{
    private const double VALOR_MAXIMO_NOTA = 10.00;
    public int AlunoId { get; private set; }
    public int AtividadeId { get; private set; }
    public double ValorNota { get; private set; }
    public DateTime DataLancamento { get; private set; }
    public int UsuarioId { get; private set; }
    public bool CanceladaPorRetentativa { get; private set; }
    public bool Cancelada { get; private set; }
    public string? MotivoCancelamento { get; private set; }
    public StatusIntegracao StatusIntegracao { get; private set; }
    public Nota(NotaParams notaParams)
        : this(notaParams.AlunoId, notaParams.AtividadeId, notaParams.ValorNota, notaParams.DataLancamento,
              notaParams.UsuarioId, notaParams.StatusIntegracao)
    {
        AlunoId = notaParams.AlunoId;
        AtividadeId = notaParams.AtividadeId;
        ValorNota = notaParams.ValorNota;
        DataLancamento = notaParams.DataLancamento;
        UsuarioId = notaParams.UsuarioId;
        CanceladaPorRetentativa = false;
        StatusIntegracao = notaParams.StatusIntegracao;
        DataCriacao = DateTime.Now;
        //Validar();

        Validar(this, NotaValidador.Instance);
    }

    private Nota(int alunoId, int atividadeId, double valorNota, DateTime dataLancamento, int usuarioId, StatusIntegracao statusIntegracao)
    {
        AlunoId = alunoId;
        AtividadeId = atividadeId;
        ValorNota = valorNota;
        DataLancamento = dataLancamento;
        UsuarioId= usuarioId;
        StatusIntegracao = statusIntegracao;
    }

    protected Nota() { }

    private void Validar()
        => ValidacoesDominio
            .Validar(this, NotaValidador.Instance);

    public void Cancelar(string motivoCancelamento)
    {
        if (string.IsNullOrWhiteSpace(motivoCancelamento))
        {
            Notificar(new(nameof(MotivoCancelamento), ConstantesDominio.MensagemValidacoes.ERRO_MOTIVO_CANCELAMENTO_NAO_INFORMADO));
            EhValida = false;
            return;
        }
        MotivoCancelamento = motivoCancelamento;
        Cancelada = true;
        DataAtualizacao = DateTime.Now;
        Validar(this, NotaValidador.Instance);
    }

    public void CancelarPorRetentativa()
    {
        MotivoCancelamento = ConstantesDominio.Mensagens.NOTA_CANCELADA_POR_RETENTATIVA;
        Cancelada = true;
        CanceladaPorRetentativa = true;
        DataAtualizacao = DateTime.Now;
        Validar();
    }

    public void AtualizarValorNota(double novoValorNota)
    {
        ValorNota = novoValorNota;
        DataAtualizacao = DateTime.Now;
        Validar();
    }

    public void AlterarStatusIntegracaoParaEnviada()
    {
        ValidarStatus(PodeAlterarStatusParaEnviado, StatusIntegracao.EnviadaParaintegracao);
        if(Notificacoes.Any())
        {
            EhValida = false;
            return;
        }
        StatusIntegracao = StatusIntegracao.EnviadaParaintegracao;
        DataAtualizacao = DateTime.Now;
        Validar();
    }

    public void AlterarStatusIntegracaoParaFalhaIntegracao()
    {
        ValidarStatus(PodeAlterarStatusParaFalhaIntegracao, StatusIntegracao.FalhaNaIntegracao);
        if (Notificacoes.Any())
        {
            EhValida = false;
            return;
        }
        StatusIntegracao = StatusIntegracao.FalhaNaIntegracao;
        DataAtualizacao = DateTime.Now;
        Validar();
    }

    public void AlterarStatusIntegracaoParaComSucesso()
    {
        ValidarStatus(PodeAlterarStatusParaIntegradaComSucesso, StatusIntegracao.IntegradaComSucesso);
        if (Notificacoes.Any())
        {
            EhValida = false;
            return;
        }
        StatusIntegracao = StatusIntegracao.IntegradaComSucesso;
        DataAtualizacao = DateTime.Now;
        Validar();
    }

    private void ValidarStatus(Func<bool> podeAlterarStatus, StatusIntegracao proximoStatus)
    {
        if (!podeAlterarStatus())
            Notificar(new(nameof(StatusIntegracao),
                string.Format(ConstantesDominio.Mensagens.ALTERACAO_DE_STATUS_NAO_PERMITIDA, proximoStatus.ToString())));
    }
}
