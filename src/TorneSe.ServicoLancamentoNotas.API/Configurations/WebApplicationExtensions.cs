using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using TorneSe.ServicoLancamentoNotas.App.Configurations.Swagger;
using TorneSe.ServicoLancamentoNotas.App.Middlewares;
using TorneSe.ServicoLancamentoNotas.Infra.Data.Configuracoes;

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

        //app.UseHealthChecks("/health", new HealthCheckOptions
        //{
        //    Predicate = p => true,
        //    ResponseWriter = ResponseWriter
        //});

        return app;
    }

    private static async Task ResponseWriter(HttpContext context, HealthReport report)
    {
        var reports = new List<object>();
        foreach (var (service, reportStatus) in report.Entries)
        {
            reports.Add(new { service, status = reportStatus.Status.ToString() });
            //report.Entries.Select((service) => new { service.Key, status = service.Value.Status.ToString() });
        }

        await context.Response.WriteAsJsonAsync(reports);
    }

    public static WebApplicationBuilder UseStartup<TStatup>(this WebApplicationBuilder applicationBuilder)
    where TStatup : class, IStartupApplication
    {
        if (Activator.CreateInstance(typeof(TStatup), applicationBuilder.Configuration) is not IStartupApplication startupApplication)
        {
            throw new ArgumentException("Classe Startup.cs Inválida");
        }

        startupApplication.ConfigureServices(applicationBuilder.Services, applicationBuilder.Environment, applicationBuilder.Configuration);

        if (applicationBuilder.Environment.IsDevelopment())
            ArquivoEnv.CarregarVariaveis();

        applicationBuilder.Host.UseSerilog();

        var app = applicationBuilder.Build();

        startupApplication.Configure(app, app.Environment);

        app.Run();

        return applicationBuilder;
    }
}
