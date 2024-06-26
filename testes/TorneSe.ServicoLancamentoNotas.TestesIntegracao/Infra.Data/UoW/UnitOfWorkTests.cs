﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Interfaces;
using TorneSe.ServicoLancamentoNotas.Infra.Data.Contexto;
using TorneSe.ServicoLancamentoNotas.Infra.Data.UoW;
using Xunit;

namespace TorneSe.ServicoLancamentoNotas.TestesIntegracao.Infra.Data.UoW;

[Collection(nameof(UnitOfWorkTestsFixture))]
public class UnitOfWorkTests
{
    private readonly UnitOfWorkTestsFixture _fixture;
    private ServicoLancamentoNotaDbContext _context; 
    private readonly IUnitOfWork _sut;


    public UnitOfWorkTests(UnitOfWorkTestsFixture fixture)
    {
        _fixture = fixture;
        _context = _fixture.CriarDbContext();
        _sut = new UnitOfWork(_context);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [Fact(DisplayName = nameof(RollBack_QuandoInvocado_NaoDeveLancarExcessao))]
    [Trait("Infra.Data", "Integracao/UoW - Unit Of Work")]
    public async Task RollBack_QuandoInvocado_NaoDeveLancarExcessao()
    {
        await _sut.Awaiting(x => x.Rollback(CancellationToken.None))
             .Should().NotThrowAsync();
    }

    [Fact(DisplayName = nameof(Commit_QuandoRealizadaPersistencia_DeveRetornarVerdadeiro))]
    [Trait("Infra.Data", "Integracao/UoW - Unit Of Work")]
    public async Task Commit_QuandoRealizadaPersistencia_DeveRetornarVerdadeiro()
    {
        var notas = _fixture.RetornarNotas();
        await _context.AddRangeAsync(notas);

        var resultado = await _sut.Commit(CancellationToken.None);

        var notasSalvas = await _context.Notas.ToListAsync();
        resultado.Should().BeTrue();
        notasSalvas.Should().NotBeEmpty();
    }

    [Fact(DisplayName = nameof(Commit_QuandoNaoExisteNotasParaPersistir_DeveRetornarFalso))]
    [Trait("Infra.Data", "Integracao/UoW - Unit Of Work")]
    public async Task Commit_QuandoNaoExisteNotasParaPersistir_DeveRetornarFalso()
    {
        var resultado = await _sut.Commit(CancellationToken.None);
        resultado.Should().BeFalse();
    }


}
