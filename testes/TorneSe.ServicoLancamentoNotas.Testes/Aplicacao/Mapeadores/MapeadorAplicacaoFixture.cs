using System;
using TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Lancar.DTOs;
using TorneSe.ServicoLancamentoNotas.Dominio.Entidades;
using TorneSe.ServicoLancamentoNotas.Dominio.Params;
using TorneSe.ServicoLancamentoNotas.Testes.Comum;
using Xunit;

namespace TorneSe.ServicoLancamentoNotas.Testes.Aplicacao.Mapeadores;

[CollectionDefinition(nameof(MapeadorAplicacaoFixture))]
public class MapeadorAplicacaoFixtureCollection
    : ICollectionFixture<MapeadorAplicacaoFixture>
{ }

public class MapeadorAplicacaoFixture
    : BaseFixture
{

    public LancarNotaInput DevolveNotaInputValido()
        => new(RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(),
            RetornaValorNotaAleatorioValido(), RetornaBoleanoRandomico());

    public NotaParams RetornaValoresParametrosNotaValidos()
        => new(RetornaNumeroIdRandomico(), RetornaNumeroIdRandomico(),
                                    RetornaValorNotaAleatorioValido(), DateTime.Now);

    public Nota RetornaNotaValida()
        => new(RetornaValoresParametrosNotaValidos());
}
