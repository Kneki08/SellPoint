using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SellPoint.View.Mappers.Categoria;
using SellPoint.View.Mappers.Cupon;
using SellPoint.View.Services;
using SellPoint.View.Services.CategoriaApiClient;
using SellPoint.View.Services.CuponApiClient;
using System.Text.Json;

namespace SellPoint.View.Extensions
{
    public static class ServicesExtensions
    {
        public static void Configure(IServiceCollection services, ApiSettings apiSettings)
        {
            // Registrar ApiSettings para inyección
            services.AddSingleton(apiSettings);

            // JSON Global
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            });

            // Logging
            services.AddLogging(config =>
            {
                config.AddConsole();
                config.SetMinimumLevel(LogLevel.Information);
            });

            // HttpClient y HttpServiceBase
            services.AddSingleton(new HttpClient());
            services.AddSingleton<HttpServiceBase>();

            // Mappers
            services.AddSingleton<ICategoriaMapper, CategoriaMapper>();
            services.AddSingleton<ICuponMapper, CuponMapper>();

            // API Clients
            services.AddSingleton<ICategoriaApiClient, CategoriaApiClient>();
            services.AddSingleton<ICuponApiClient, CuponApiClient>();

            // Formularios
            services.AddTransient<FormCategoria>();
            services.AddTransient<FormCupon>();
        }
    }
}

