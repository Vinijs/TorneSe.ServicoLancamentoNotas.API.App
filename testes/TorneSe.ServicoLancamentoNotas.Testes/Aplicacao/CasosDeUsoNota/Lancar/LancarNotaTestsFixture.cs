using System;
using TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Lancar.DTOs;
using TorneSe.ServicoLancamentoNotas.Dominio.Entidades;
using TorneSe.ServicoLancamentoNotas.Dominio.Params;
using TorneSe.ServicoLancamentoNotas.Testes.Comum;
using Xunit;

namespace TorneSe.ServicoLancamentoNotas.Testes.Aplicacao.CasosDeUsoNota.Lancar;

[CollectionDefinition(nameof(LancarNotaTestsFixture))]
public class LancarNotaTestsFixtureCollection
    : ICollectionFixture<LancarNotaTestsFixture>
{ }

public class LancarNotaTestsFixture
    : BaseFixture
{
    public NotaParams RetornaValoresParametrosNotaValidos()
        => new(RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(),
                                    RetornaValorNotaAleatorioValido(), DateTime.Now);

    public Nota RetornaNota()
        => new(RetornaValoresParametrosNotaValidos());

    public LancarNotaInput DevolveNotaInputValido()
        => new(RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(),
            RetornaValorNotaAleatorioValido(), false);

    public LancarNotaInput DevolveNotaInputValidoSubstitutivo()
        => new(RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(),
            RetornaValorNotaAleatorioValido(), true);

    public LancarNotaInput DevolveNotaInputInValido()
        => new(-1, -1, -1, 11, false);
}
