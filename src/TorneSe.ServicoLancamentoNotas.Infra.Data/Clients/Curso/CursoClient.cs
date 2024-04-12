using Microsoft.Extensions.Logging;
using System.Text.Json;
using TorneSe.ServicoLancamentoNotas.Dominio.Clients;
using ValueObjects = TorneSe.ServicoLancamentoNotas.Dominio.ValueObjects;

namespace TorneSe.ServicoLancamentoNotas.Infra.Data.Clients.Curso;

public class CursoClient : ICursoClient
{
    private readonly HttpClient _client;
    private readonly ILogger<CursoClient> _logger;

    public CursoClient(HttpClient client, ILogger<CursoClient> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<ValueObjects.Curso?> ObterInformacoesCursoAluno(int alunoId, int professorId, 
        int atividadeId, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, 
            $"obterCurso?alunoId={alunoId}&professorId={professorId}&atividadeid{atividadeId}");

        var resultado = await _client.SendAsync(request, cancellationToken);

        if (!resultado.IsSuccessStatusCode)
        {
            _logger
                .LogError($"Ocorreu um erro ao consultar o cursodo aluno {alunoId}. Informações resposta {{@resultado}}", 
                resultado);
            return default;
        }

        return JsonSerializer.Deserialize<ValueObjects.Curso>(await resultado.Content.ReadAsStringAsync(cancellationToken));
    }
}
