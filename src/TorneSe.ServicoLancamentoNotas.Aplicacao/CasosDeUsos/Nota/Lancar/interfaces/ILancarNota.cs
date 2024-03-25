using MediatR;
using TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Comum;
using TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Lancar.DTOs;

namespace TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Lancar.interfaces;

public interface ILancarNota : IRequestHandler<LancarNotaInput, NotaOutputModel> {}
