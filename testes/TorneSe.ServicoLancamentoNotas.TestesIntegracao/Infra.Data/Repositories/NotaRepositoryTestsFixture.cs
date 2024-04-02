using TorneSe.ServicoLancamentoNotas.TestesIntegracao.Base;
using Xunit;

namespace TorneSe.ServicoLancamentoNotas.TestesIntegracao.Infra.Data.Repositories;

[CollectionDefinition(nameof(NotaRepositoryTestsFixture))]
public class NotaRepositoryTestsFixtureCollection
    : ICollectionFixture<NotaRepositoryTestsFixture>
{ }

public class NotaRepositoryTestsFixture 
    : BaseFixture
{

}