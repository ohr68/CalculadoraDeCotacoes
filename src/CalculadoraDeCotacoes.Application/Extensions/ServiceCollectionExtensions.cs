using CalculadoraDeCotacoes.Application.Common;
using CalculadoraDeCotacoes.Application.Common.Interfaces;
using CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace CalculadoraDeCotacoes.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services
            .AddValidation()
            .AddHelpers()
            .ConfigureMapster();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
    
    private static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddScoped<IFaixaDeIdadeHelper, FaixaDeIdadeHelper>();
        
        return services;
    }
    
    private static IServiceCollection ConfigureMapster(this IServiceCollection services)
    {
        services.AddMapster();

        TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IncluirCotacaoValidator>();

        return services;
    }
}