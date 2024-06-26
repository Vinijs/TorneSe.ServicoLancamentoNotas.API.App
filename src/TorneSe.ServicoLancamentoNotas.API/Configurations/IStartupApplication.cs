﻿namespace TorneSe.ServicoLancamentoNotas.App.Configurations;

public interface IStartupApplication
{
    IConfiguration Configuration { get; }
    void ConfigureServices(IServiceCollection services, IHostEnvironment environment, IConfiguration configuration);
    void Configure(WebApplication app, IWebHostEnvironment env);
}
