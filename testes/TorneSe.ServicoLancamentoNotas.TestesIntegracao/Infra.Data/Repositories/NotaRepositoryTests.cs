﻿using TorneSe.ServicoLancamentoNotas.Dominio.Repositories;
using TorneSe.ServicoLancamentoNotas.Infra.Data.Contexto;
using TorneSe.ServicoLancamentoNotas.Infra.Data.Repositories;
using InfraRepos = TorneSe.ServicoLancamentoNotas.Infra.Data.Repositories;
using Xunit;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System.Linq;
using System;

namespace TorneSe.ServicoLancamentoNotas.TestesIntegracao.Infra.Data.Repositories;

[Collection(nameof(NotaRepositoryTestsFixture))]
public class NotaRepositoryTests
{
    private readonly INotaRepository _sut;
    private readonly NotaRepositoryTestsFixture _fixture;
    private ServicoLancamentoNotaDbContext _context;

    public NotaRepositoryTests(NotaRepositoryTestsFixture fixture)
    {
        _fixture = fixture;
        _context = _fixture.CriarDbContext();
        _sut = new InfraRepos.NotaRepository(_context);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [Fact(DisplayName = nameof(Inserir_QuandoFornecidaNota_DeveSalvar))]
    [Trait("Infra.Data", "Integracao/Repositories - Nota Repositoriy")]
    public async Task Inserir_QuandoFornecidaNota_DeveSalvar()
    {
        //Arrange
        var nota = _fixture.RetornaNota();

        //Act
        await _sut.Inserir(nota, CancellationToken.None);
        await _context.SaveChangesAsync();

        //Assert
        var notaSalva = _context.Notas.FirstOrDefault(x => x.Id == nota.Id);
        notaSalva.Should().NotBeNull();
        notaSalva!.AlunoId.Should().Be(nota.AlunoId);
        notaSalva.AtividadeId.Should().Be(nota.AtividadeId);
        notaSalva.ValorNota.Should().Be(nota.ValorNota);
        notaSalva.DataLancamento.Should().Be(nota.DataLancamento);
        notaSalva.DataCriacao.Should().BeCloseTo(nota.DataCriacao, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName = nameof(BuscarNotaPorAlunoEAtividade_QuandoNotaExiste_DeveRetornar))]
    [Trait("Infra.Data", "Integracao/Repositories - Nota Repositoriy")]
    public async Task BuscarNotaPorAlunoEAtividade_QuandoNotaExiste_DeveRetornar()
    {
        //Arrange
        var nota = _fixture.RetornaNota();
        await _context.Notas.AddAsync(nota);
        await _context.SaveChangesAsync();

        //Act
        var notaSalva = await _sut.BuscarNotaPorAlunoEAtividade(nota.AlunoId, nota.AtividadeId, CancellationToken.None);

        //Assert
        notaSalva.Should().NotBeNull();
        notaSalva!.AlunoId.Should().Be(nota.AlunoId);
        notaSalva.AtividadeId.Should().Be(nota.AtividadeId);
        notaSalva.ValorNota.Should().Be(nota.ValorNota);
        notaSalva.DataLancamento.Should().Be(nota.DataLancamento);
        notaSalva.DataCriacao.Should().BeCloseTo(nota.DataCriacao, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName = nameof(BuscarNotaPorAlunoEAtividade_QuandoNotaExiste_DeveRetornar))]
    [Trait("Infra.Data", "Integracao/Repositories - Nota Repositoriy")]
    public async Task Atualizar_QuandoNotaExiste_DeveAtualizarValores()
    {
        //Arrange
        var novoValorNota = 10;
        var nota = _fixture.RetornaNota();
        var tracking = await _context.Notas.AddAsync(nota);
        await _context.SaveChangesAsync();
        tracking.State = EntityState.Detached;
        nota!.AtualizarValorNota(novoValorNota);

        //Act
        await _sut.Atualizar(nota!, CancellationToken.None);
        await _context.SaveChangesAsync();

        var notas = await _context.Notas.ToListAsync();
        var notaSalva = notas.FirstOrDefault(x => x.Id == nota.Id);

        //Assert
        notaSalva.Should().NotBeNull();
        notaSalva!.AlunoId.Should().Be(nota.AlunoId);
        notaSalva.AtividadeId.Should().Be(nota.AtividadeId);
        notaSalva.ValorNota.Should().Be(novoValorNota);
        notaSalva.DataLancamento.Should().Be(nota.DataLancamento);
        notaSalva.DataCriacao.Should().BeCloseTo(nota.DataCriacao, TimeSpan.FromSeconds(1));
    }
}