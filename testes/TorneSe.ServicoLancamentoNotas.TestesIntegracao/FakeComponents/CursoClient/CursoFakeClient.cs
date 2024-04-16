using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TorneSe.ServicoLancamentoNotas.Dominio.Clients;
using TorneSe.ServicoLancamentoNotas.Dominio.ValueObjects;
using TorneSe.ServicoLancamentoNotas.TestesIntegracao.FakeComponents.Fakers;

namespace TorneSe.ServicoLancamentoNotas.TestesIntegracao.FakeComponents.CursoClient;

public class CursoFakeClient : ICursoClient
{
    public Task<IEnumerable<Curso>>? ObterInformacoesCursoAluno(int alunoId, int professorId, int atividadeId, CancellationToken cancellationToken) 
        => Task.FromResult(CursoFake.ObterCursoAluno(atividadeId, alunoId, professorId));
}
