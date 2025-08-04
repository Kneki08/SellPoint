using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using SellPoint.View.Factories;
using SellPoint.View.Forms;
using SellPoint.View.Mappers;
using SellPoint.View.Services.Pedido;
using SellPoint.View.Validations;

namespace SellPoint.View.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPedidoDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración JSON global
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            });

            // HttpClient configurado por DI con URL base
            services.AddHttpClient<IPedidoApiClient, PedidoApiClient>(client =>
            {
                var baseUrl = configuration["ApiSettings:PedidoBaseUrl"]
                    ?? throw new InvalidOperationException("Falta la ruta PedidoBaseUrl en appsettings.json");

                client.BaseAddress = new Uri(baseUrl);
            });

            // Registro de servicios y formulario
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<PedidoForm>();
            services.AddScoped<IPedidoDtoFactory, PedidoDtoFactory>();
            services.AddScoped<IPedidoFormMapper, PedidoFormMapper>();
            services.AddScoped<IPedidoValidator, PedidoValidator>();
            services.AddScoped<IPedidoViewModelMapper, PedidoViewModelMapper>();
            services.AddScoped<IPedidoCamposService, PedidoCamposService>();

            return services;
        }
    }
}