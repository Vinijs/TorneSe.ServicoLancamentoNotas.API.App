using TorneSe.ServicoLancamentoNotas.App.Configurations.Swagger;
using TorneSe.ServicoLancamentoNotas.App.Extensions;
using TorneSe.ServicoLancamentoNotas.App.Filters;
using TorneSe.ServicoLancamentoNotas.App.Middlewares;
using TorneSe.ServicoLancamentoNotas.Infra.CrossCutting.IoC;

namespace TorneSe.ServicoLancamentoNotas.App.Configurations;
public static class ConfigurarInjecaoDependenciaExtension
{
    public static IServiceCollection ConfigurarServicos(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ApiGlobalExceptionFilter));
        })
        .AdicionarSerializerContext();
        services.RegistrarServicos();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddScoped<BuscaTenantMiddleware>();
        services.AdicionarConfiguracoesSwagger();
        return services;
    }
}
