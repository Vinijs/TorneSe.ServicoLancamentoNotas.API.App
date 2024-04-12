using FluentAssertions;
using FluentValidation;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Behaviors;
using TorneSe.ServicoLancamentoNotas.Aplicacao.CasosDeUsos.Nota.Lancar.DTOs;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Enums;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Validacoes;
using Xunit;

namespace TorneSe.ServicoLancamentoNotas.Testes.Aplicacao.Behaviors;

[Collection(nameof(LancarNotaInputValidacaoBehaviorTestsFixture))]
public class LancarNotaInputValidacaoBehaviorTests
{
    private readonly LancarNotaInputValidacaoBehaviorTestsFixture _fixture;
    private readonly Mock<IValidator<LancarNotaInput>> _validadorMock;
    private LancarNotaInputValidacaoBehavior _sut;

    public LancarNotaInputValidacaoBehaviorTests(LancarNotaInputValidacaoBehaviorTestsFixture fixture)
    {
        _fixture = fixture;
        _validadorMock = new Mock<IValidator<LancarNotaInput>>();
        _sut = new LancarNotaInputValidacaoBehavior(_validadorMock.Object);
    }

    [Fact(DisplayName = nameof(Handle_QuandoValidacaoPossuiErros_DeveRetornarErro))]
    [Trait("Aplicacao", "Nota - Comportamentos")]
    public async Task Handle_QuandoValidacaoPossuiErros_DeveRetornarErro()
    {
        var request = _fixture.DevolveNotaInputInValido();
        var resultadoValidacao = await LancarNotaInputValidator.Instance.ValidateAsync(request);
        _validadorMock.Setup(x => x.ValidateAsync(It.IsAny<LancarNotaInput>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultadoValidacao);

        var resultado = await _sut.Handle(request, null!, CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.Erro.Should().Be(TipoErro.InputNotaInvalido);
        resultado.Sucesso.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Handle_QuandoValidacaoPossuiErros_DeveRetornarSucesso))]
    [Trait("Aplicacao", "Nota - Comportamentos")]
    public async Task Handle_QuandoValidacaoPossuiErros_DeveRetornarSucesso()
    {
        var request = _fixture.DevolveNotaInputValido();
        var resultadoValidacao = await LancarNotaInputValidator.Instance.ValidateAsync(request);
        _validadorMock.Setup(x => x.ValidateAsync(It.IsAny<LancarNotaInput>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultadoValidacao);


        var resultado = await _sut.Handle(request, _fixture.RetornaSucesso, CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.Sucesso.Should().BeTrue();
        resultado.Dado.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(Handle_QuandoValidadorEstaNulo_DeveRetornarSucesso))]
    [Trait("Aplicacao", "Nota - Comportamentos")]
    public async Task Handle_QuandoValidadorEstaNulo_DeveRetornarSucesso()
    {
        var request = _fixture.DevolveNotaInputValido();
        _sut = new LancarNotaInputValidacaoBehavior(null!);

        var resultado = await _sut.Handle(request, _fixture.RetornaSucesso, CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.Sucesso.Should().BeTrue();
        resultado.Dado.Should().NotBeNull();
    }
}
