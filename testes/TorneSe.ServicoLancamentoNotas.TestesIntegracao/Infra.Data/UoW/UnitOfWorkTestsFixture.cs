using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TorneSe.ServicoLancamentoNotas.Dominio.Entidades;
using TorneSe.ServicoLancamentoNotas.Infra.Data.Contexto;
using TorneSe.ServicoLancamentoNotas.TestesIntegracao.Base;
using Xunit;

namespace TorneSe.ServicoLancamentoNotas.TestesIntegracao.Infra.Data.UoW;

[CollectionDefinition(nameof(UnitOfWorkTestsFixture))]
public class UnitOfWorkTestsFixtureCollection
    : ICollectionFixture<UnitOfWorkTestsFixture>
{ }

public class UnitOfWorkTestsFixture :
    BaseFixture
{
    public List<Nota> RetornarNotas(int? quantidadeGerada = null)
        => Enumerable.Range(1, quantidadeGerada ?? 10).Select(id => RetornaNota(id)).ToList();

    public ServicoLancamentoNotaDbContext CriarDbContext()
    {

        var dbContext = new ServicoLancamentoNotaDbContext
            (
                new DbContextOptionsBuilder<ServicoLancamentoNotaDbContext>()
                    .UseInMemoryDatabase("integration-tests-unit-of-work")
                    .Options
            );

        return dbContext;
    }
}
