using Microsoft.Extensions.Logging;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Eventos;
using TorneSe.ServicoLancamentoNotas.Aplicacao.EventosHandlers.Interfaces;
using TorneSe.ServicoLancamentoNotas.Dominio.Clients;
using TorneSe.ServicoLancamentoNotas.Dominio.Mensagens;
using TorneSe.ServicoLancamentoNotas.Dominio.Repositories;

namespace TorneSe.ServicoLancamentoNotas.Aplicacao.EventosHandlers;

public class NotaAtualizadaEventoHandler : INotaAtualizadaEventoHandler
{
    private readonly INotaAtualizadaMensagemClient _mensagemClient;
    private INotaRepository _notaRepository;
    private ILogger<NotaLancadaEventoHandler> _logger;

    public NotaAtualizadaEventoHandler(INotaRepository notaRepository,
                                       ILogger<NotaLancadaEventoHandler> logger)
    {
        _notaRepository = notaRepository;
        _logger = logger;
    }

    public async Task Handle(NotaAtualizadaEvento notification, CancellationToken cancellationToken)
    {
        var nota = await _notaRepository.Buscar(notification.NotaId, cancellationToken);

        if(nota is null)
        {
            _logger.LogInformation($"A nota com id {notification.NotaId} não foi encontrada");
        }

        var mensagem = NotaAtualizadaMensagem.DeNota(nota, notification.CorrelationId);

        nota.AlterarStatusIntegracaoParaEnviada();

        await _notaRepository.Atualizar(nota, cancellationToken);
        await _mensagemClient.EnviarMensagem(mensagem);
    }
}
