using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Interfaces;
using TorneSe.ServicoLancamentoNotas.Dominio.SeedWork;

namespace TorneSe.ServicoLancamentoNotas.TestesIntegracao.FakeComponents.Mediator;

internal class MediatorFakeHandler : IMediatorHandler
{
    public Task<TResponse> EnviarRequest<TResponse, TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse> => null!;

    public Task PublicarEvento<TNotificacao>(TNotificacao notificacao, CancellationToken cancellationToken)
        where TNotificacao : Evento, INotification => Task.CompletedTask;
}
