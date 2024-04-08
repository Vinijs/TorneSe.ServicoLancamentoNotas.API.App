using TorneSe.ServicoLancamentoNotas.App.Configurations.Swagger;
using TorneSe.ServicoLancamentoNotas.App.Middlewares;
using TorneSe.ServicoLancamentoNotas.Infra.CrossCutting.IoC;

namespace TorneSe.ServicoLancamentoNotas.App.Configurations;
public static class ConfigurarInjecaoDependenciaExtension
{
    public static IServiceCollection ConfigurarServicos(this IServiceCollection services)
    {
        services.AddControllers();
        services.RegistrarServicos();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddScoped<BuscaTenantMiddleware>();
        services.AdicionarConfiguracoesSwagger();
        return services;
    }
}
