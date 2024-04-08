namespace TorneSe.ServicoLancamentoNotas.App.Configurations.Swagger;

public static class ConfiguracoesSwagger
{
    public static void AdicionarConfiguracoesSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.EnableAnnotations();
            config.OperationFilter<SwaggerValoresDefaultFilter>();
            config.OperationFilter<HeaderObrigatorioFilter>();
            
        });
    }

    public static void UserConfiguracoesSwagger(this IApplicationBuilder app)
    {
            app.UseSwagger();
            app.UseSwaggerUI();
    }
}
