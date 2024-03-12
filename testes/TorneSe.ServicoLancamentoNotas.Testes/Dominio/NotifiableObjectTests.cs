using FluentAssertions;
using System.Linq;
using TorneSe.ServicoLancamentoNotas.Dominio.SeedWork;
using TorneSe.ServicoLancamentoNotas.Testes.Fakes;
using Xunit;

namespace TorneSe.ServicoLancamentoNotas.Testes.Dominio;

public class NotifiableObjectTests
{
    [Fact(DisplayName = nameof(Notificar_DeveAdicionar_Notificacao_NaLista))]
    [Trait("Dominio", "NotifiableObject - Notificacao")]
    public void Notificar_DeveAdicionar_Notificacao_NaLista()
    {
        //Arrange
        string nomeCampo = "UsuarioId";
        string mensagem = "mensagem Teste";
        NotifiableObject objetoNotificavel = new NotaFake();

        //Act
        objetoNotificavel.Notificar(new Notificacao(nomeCampo,mensagem));

        //Asserts
        objetoNotificavel.Notificacoes.Should().NotBeEmpty();
        objetoNotificavel.Notificacoes.Should().HaveCount(1);
        objetoNotificavel.Notificacoes.First().Campo.Should().Be(nomeCampo);
        objetoNotificavel.Notificacoes.First().Mensagem.Should().Be(mensagem);
    }
}
