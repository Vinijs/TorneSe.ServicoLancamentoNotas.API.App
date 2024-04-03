using Microsoft.EntityFrameworkCore;
using TorneSe.ServicoLancamentoNotas.Infra.Data.Contexto;
using TorneSe.ServicoLancamentoNotas.TestesIntegracao.Base;
using Xunit;
namespace TorneSe.ServicoLancamentoNotas.TestesIntegracao.Aplicacao.CasosDeUso.Nota.Lancar;

[CollectionDefinition(nameof(LancarNotaTestsFixture))]

public class LancarNotaTestsFixtureCollection
    : ICollectionFixture<LancarNotaTestsFixture>
{ }

public class LancarNotaTestsFixture
    : BaseFixture
{
    public ServicoLancamentoNotaDbContext CriarDbContext()
    {
        var dbContext = new ServicoLancamentoNotaDbContext
            (
                new DbContextOptionsBuilder<ServicoLancamentoNotaDbContext>()
                    .UseInMemoryDatabase("integration-tests")
                    .Options
            );
        return dbContext;
    }
}
