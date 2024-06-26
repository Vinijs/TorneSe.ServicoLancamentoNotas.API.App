﻿using System.Runtime.Serialization;
using TorneSe.ServicoLancamentoNotas.Dominio.ValueObjects;
using TorneSe.ServicoLancamentoNotas.Infra.Data.Constantes;
using TorneSe.ServicoLancamentoNotas.Infra.Data.Providers.Interfaces;

namespace TorneSe.ServicoLancamentoNotas.Infra.Data.Providers;

public class VariaveisAmbienteProvider : IVariaveisAmbienteProvider
{
    private readonly int TIMEOUT_PADRAO = 14;
    private readonly int NUMERO_RETENTATIVAS_PADRAO = 4;
    private readonly int DURACAO_CIRCUITO_ABERTO_PADRAO = 60;
    private readonly int NUMERO_ERROS_PARA_ABERTURA_CIRCUITO_PADRAO = 10;
    private readonly int WAIT_TIME_SECONDS_DEFAULT = 30;
    
    public static readonly VariaveisAmbienteProvider Instance = new VariaveisAmbienteProvider();
    public HashSet<string> Tenants => _tenants;

    public string? UrlBaseCursos => Buscar(VariaveisAmbienteConstantes.URL_BASE_CURSOS);

    public string? PathObtercursos => Buscar(VariaveisAmbienteConstantes.PATH_OBTER_CURSOS);

    public int ErrosAntesDeAbrirCircuito 
        => int.TryParse(Buscar(VariaveisAmbienteConstantes.NUMERO_ERROS_PARA_ABERTURA_CIRCUITO), 
            out int erros) ? erros : NUMERO_ERROS_PARA_ABERTURA_CIRCUITO_PADRAO;

    public TimeSpan DuracaoCircuito
        => int.TryParse(Buscar(VariaveisAmbienteConstantes.DURACAO_CIRCUITO_ABERTO),
            out int duracaoCircuito) ? TimeSpan.FromSeconds(duracaoCircuito) 
        : TimeSpan.FromSeconds(DURACAO_CIRCUITO_ABERTO_PADRAO);


    public int NumeroRetentativas 
        => int.TryParse(Buscar(VariaveisAmbienteConstantes.NUMERO_RETENTATIVAS),
            out int retentativas) ? retentativas : NUMERO_RETENTATIVAS_PADRAO;

    public int Timeout 
        => int.TryParse(Buscar(VariaveisAmbienteConstantes.TIMEOUT),
            out int timeout) ? timeout : TIMEOUT_PADRAO;

    public int WaitTimeSeconds => int.TryParse(Buscar(VariaveisAmbienteConstantes.WAIT_TIME_SECONDS),
            out int segundos) ? segundos : WAIT_TIME_SECONDS_DEFAULT;

    public string? AwsAccessKey => Buscar(VariaveisAmbienteConstantes.AWS_ACCESS_KEY);

    public string? AwsSecretAccessKey => Buscar(VariaveisAmbienteConstantes.AWS_SECRET_ACCESS_KEY);
    public string ElasticSearchUrl => Buscar(VariaveisAmbienteConstantes.ELASTIC_SEARCH_URL)!;

    private readonly HashSet<string> _tenants;

    public VariaveisAmbienteProvider()
    {
        _tenants = new HashSet<string>(RetornaTenants());
    }

    public string? ObterConnectionStringPorTenant(Tenant tenant)
    {
        var nomeTenant = tenant.ToString().Replace("-", "").ToUpperInvariant();
        var variavelAmbiente = $"{VariaveisAmbienteConstantes.CONNECTION_STRING_PREFIX}{nomeTenant}";
        return Buscar(variavelAmbiente);
    }

    private IEnumerable<string> RetornaTenants()
    {
        var tenants = Buscar(VariaveisAmbienteConstantes.TENANTS);

        if (string.IsNullOrWhiteSpace(tenants))
            return new HashSet<string>();

        return new HashSet<string>(tenants!.Split(VariaveisAmbienteConstantes.SEPARADOR_TENANTS,
                                    StringSplitOptions.RemoveEmptyEntries));
    }

    private string? Buscar(string nome)
        => Environment.GetEnvironmentVariable(nome);

    public string? ObterNomeFila(string nomeFila) => Buscar(nomeFila);
}
