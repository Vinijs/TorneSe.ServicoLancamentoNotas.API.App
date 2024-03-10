namespace TorneSe.ServicoLancamentoNotas.App.Configurations;

public static class WebApplicationExtensions
{
    public static WebApplication UsarServicos(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

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

        startupApplication.ConfigureServices(applicationBuilder.Services);

        var app = applicationBuilder.Build();

        startupApplication.Configure(app, app.Environment);

        app.Run();

        return applicationBuilder;
    }
}
