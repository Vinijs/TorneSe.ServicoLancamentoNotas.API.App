﻿using System;
using TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Cancelar.DTOs;
using TorneSe.ServicoLancamentoNotas.Dominio.Entidades;
using TorneSe.ServicoLancamentoNotas.Dominio.Params;
using TorneSe.ServicoLancamentoNotas.Testes.Comum;
using Xunit;

namespace TorneSe.ServicoLancamentoNotas.Testes.Aplicacao.CasosDeUsoNota.Cancelar;

[CollectionDefinition(nameof(CancelarNotaTestsFixture))]
public class CancelarNotaTestsFixtureCollection
    : ICollectionFixture<CancelarNotaTestsFixture>
{ }
public class CancelarNotaTestsFixture
    : BaseFixture
{
    public NotaParams RetornaValoresParametrosNotaValidos()
        => new(RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(),
                                    RetornaValorNotaAleatorioValido(), DateTime.Now);

    public Nota RetornaNota()
        => new(RetornaValoresParametrosNotaValidos());

    public CancelarNotaInput RetornaInput()
        => new(RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(), Faker.Commerce.ProductName());
}
