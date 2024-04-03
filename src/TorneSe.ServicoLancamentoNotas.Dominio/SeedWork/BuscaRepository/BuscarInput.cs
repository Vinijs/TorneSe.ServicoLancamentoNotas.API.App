using TorneSe.ServicoLancamentoNotas.Dominio.Enums;

namespace TorneSe.ServicoLancamentoNotas.Dominio.SeedWork.BuscaRepository;

public record struct BuscarInput
    (int Pagina, int PorPagina, int? AlunoId, int? AtividadeId, string OrdenarPor, OrdenacaoBusca Ordenacao = OrdenacaoBusca.Asc);
