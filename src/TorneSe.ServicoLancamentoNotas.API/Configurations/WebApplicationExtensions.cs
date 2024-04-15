﻿using TorneSe.ServicoLancamentoNotas.App.Configurations.Swagger;
using TorneSe.ServicoLancamentoNotas.App.Middlewares;

namespace TorneSe.ServicoLancamentoNotas.App.Configurations;

public static class WebApplicationExtensions
{
    public static WebApplication UsarServicos(this WebApplication app)
    {
        app.UserConfiguracoesSwagger();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<BuscaTenantMiddleware>();

        app.MapControllers();

        return app;
    }

    public static WebApplicationBuilder UseStartup<TStatup>(this WebApplicationBuilder applicationBuilder)
    where TStatup : class, IStartupApplication
    {
        if (Activator.CreateInstance(typeof(TStatup), applicationBuilder.Configuration) is not IStartupApplication startupApplication)
        {
            throw new ArgumentException("Classe Startup.cs Inválida");
        }

        Environment.SetEnvironmentVariable("TENANTS", "torne-se-csharp;torne-se-javascript;torne-se-java");
        Environment.SetEnvironmentVariable("CONNECTION_STRING_TORNESECSHARP", "Server=localhost;Database=TorneSeCsharp;Port=3308;Uid=root;Pwd=root;Pooling=True;");
        Environment.SetEnvironmentVariable("CONNECTION_STRING_TORNESEJAVA", "Server=localhost;Database=TorneSeJava;Port=3308;Uid=root;Pwd=root;Pooling=True;");
        Environment.SetEnvironmentVariable("CONNECTION_STRING_TORNESEJAVASCRIPT", "Server=localhost;Database=TorneSeJavaScript;Port=3308;Uid=root;Pwd=root;Pooling=True;");
        Environment.SetEnvironmentVariable("URL_BASE_CURSOS", "https://localhost:7295/");
        Environment.SetEnvironmentVariable("PATH_OBTER_CURSOS", "obterCurso");
        Environment.SetEnvironmentVariable("TIMEOUT", "7");
        Environment.SetEnvironmentVariable("NUMERO_RETENTATIVAS", "3");
        Environment.SetEnvironmentVariable("DURACAO_CIRCUITO_ABERTO", "30");
        Environment.SetEnvironmentVariable("NUMERO_ERROS_PARA_ABERTURA_CIRCUITO", "6");


        startupApplication.ConfigureServices(applicationBuilder.Services);

        var app = applicationBuilder.Build();

        startupApplication.Configure(app, app.Environment);

        app.Run();

        return applicationBuilder;
    }
}
