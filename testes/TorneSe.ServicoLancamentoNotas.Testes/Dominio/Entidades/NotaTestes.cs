using System;
using TorneSe.ServicoLancamentoNotas.Dominio.Constantes;
using TorneSe.ServicoLancamentoNotas.Dominio.Entidades;
using TorneSe.ServicoLancamentoNotas.Dominio.Enums;
using TorneSe.ServicoLancamentoNotas.Dominio.Exceptions;
using Xunit;

namespace TorneSe.ServicoLancamentoNotas.Testes.Dominio.Entidades;

public class NotaTestes
{
    [Fact(DisplayName = nameof(InstanciarNota))]
    [Trait("Dominio", "Nota - Agregado")]
    public void InstanciarNota()
    {
        //Arrange
        var valoresEntrada = new 
        {
            AlunoId = 1234,
            AtividadeId = 34566,
            ValorNota = 10.00,
            DataLancamento = DateTime.Now,
            UsuarioId = 34566,
            DataAtualizacao = DateTime.Now,
        };

        //Act
        var nota = new Nota(valoresEntrada.AlunoId, valoresEntrada.AtividadeId,
                            valoresEntrada.ValorNota, valoresEntrada.DataLancamento, valoresEntrada.UsuarioId);

        //Assert
        Assert.NotNull(nota);
        Assert.Equal(valoresEntrada.AlunoId, nota.AlunoId);
        Assert.Equal(valoresEntrada.AtividadeId, nota.AtividadeId);
        Assert.Equal(valoresEntrada.ValorNota, nota.ValorNota);
        Assert.Equal(valoresEntrada.DataLancamento, nota.DataLancamento);
        Assert.NotEqual(default(DateTime), nota.DataLancamento);
        Assert.NotEqual(default(DateTime), nota.DataAtualizacao);
        Assert.Equal(valoresEntrada.UsuarioId, nota.UsuarioId);
        Assert.False(nota.CanceladaPorRetentativa);
        Assert.Equal(StatusIntegracao.AguardandoIntegracao, nota.StatusIntegracao);
        Assert.Null(nota.MotivoCancelamento);
    }

    [Theory(DisplayName = nameof(InstanciarNota_QuandoValorNotaInvalido_DeveLancarExcecao))]
    [Trait("Dominio", "Nota - Agregado")]
    [InlineData(-1)]
    [InlineData(11)]
    public void InstanciarNota_QuandoValorNotaInvalido_DeveLancarExcecao(double valorNota)
    {
        //Arrange
        var valoresEntrada = new
        {
            AlunoId = 1234,
            AtividadeId = 34566,
            ValorNota = valorNota,
            DataLancamento = DateTime.Now,
            UsuarioId = 34566,
            DataAtualizacao = DateTime.Now,
        };

        //Act
        var action = () => new Nota(valoresEntrada.AlunoId, valoresEntrada.AtividadeId,
                            valoresEntrada.ValorNota, valoresEntrada.DataLancamento, valoresEntrada.UsuarioId);

        var exception = Assert.Throws<ValidacaoEntidadeException>(action);
        Assert.NotNull(exception);
        Assert.Equal(ConstantesDominio.MensagemValidacoes.ERRO_VALOR_NOTA_INVALIDO, exception.Message);

        //Assert

    }

    [Theory(DisplayName = nameof(InstanciarNota_QuandoUsuarioIdInvalido_DeveLancarExcecao))]
    [Trait("Dominio", "Nota - Agregado")]
    [InlineData(-1)]
    [InlineData(0)]
    public void InstanciarNota_QuandoUsuarioIdInvalido_DeveLancarExcecao(int usuarioId)
    {
        //Arrange
        var valoresEntrada = new
        {
            AlunoId = 1234,
            AtividadeId = 34566,
            ValorNota = 10,
            DataLancamento = DateTime.Now,
            UsuarioId = usuarioId,
            DataAtualizacao = DateTime.Now,
        };

        //Act
        var action = () => new Nota(valoresEntrada.AlunoId, valoresEntrada.AtividadeId,
                            valoresEntrada.ValorNota, valoresEntrada.DataLancamento, valoresEntrada.UsuarioId);

        var exception = Assert.Throws<ValidacaoEntidadeException>(action);
        Assert.NotNull(exception);
        Assert.Equal(ConstantesDominio.MensagemValidacoes.ERRO_USUARIO_INVALIDO, exception.Message);

        //Assert

    }

    [Theory(DisplayName = nameof(InstanciarNota_QuandoAlunoIdInvalido_DeveLancarExcecao))]
    [Trait("Dominio", "Nota - Agregado")]
    [InlineData(-1)]
    [InlineData(0)]
    public void InstanciarNota_QuandoAlunoIdInvalido_DeveLancarExcecao(int alunoId)
    {
        //Arrange
        var valoresEntrada = new
        {
            AlunoId = alunoId,
            AtividadeId = 34566,
            ValorNota = 10,
            DataLancamento = DateTime.Now,
            UsuarioId = 34566,
            DataAtualizacao = DateTime.Now,
        };

        //Act
        var action = () => new Nota(valoresEntrada.AlunoId, valoresEntrada.AtividadeId,
                            valoresEntrada.ValorNota, valoresEntrada.DataLancamento, valoresEntrada.UsuarioId);

        var exception = Assert.Throws<ValidacaoEntidadeException>(action);
        Assert.NotNull(exception);
        Assert.Equal(ConstantesDominio.MensagemValidacoes.ERRO_ALUNO_INVALIDO, exception.Message);

        //Assert

    }

    [Theory(DisplayName = nameof(InstanciarNota_QuandoAtividadeIdInvalido_DeveLancarExcecao))]
    [Trait("Dominio", "Nota - Agregado")]
    [InlineData(-1)]
    [InlineData(0)]
    public void InstanciarNota_QuandoAtividadeIdInvalido_DeveLancarExcecao(int atividadeId)
    {
        //Arrange
        var valoresEntrada = new
        {
            AlunoId = 1234,
            AtividadeId = atividadeId,
            ValorNota = 10,
            DataLancamento = DateTime.Now,
            UsuarioId = 34566,
            DataAtualizacao = DateTime.Now,
        };

        //Act
        var action = () => new Nota(valoresEntrada.AlunoId, valoresEntrada.AtividadeId,
                            valoresEntrada.ValorNota, valoresEntrada.DataLancamento, valoresEntrada.UsuarioId);

        var exception = Assert.Throws<ValidacaoEntidadeException>(action);
        Assert.NotNull(exception);
        Assert.Equal(ConstantesDominio.MensagemValidacoes.ERRO_ATIVIDADE_INVALIDA, exception.Message);

        //Assert

    }

    //Preciso controlar a nota lançada já foi integrada
    //Caso uma nota venha a ser cancelada preciso de um motivo para o cancelamento
    //Um valor de nota deve estar no intervalo entre 0 a 10
}
