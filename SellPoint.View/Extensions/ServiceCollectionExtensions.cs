using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using SellPoint.View.Factories;
using SellPoint.View.Forms;
using SellPoint.View.Mappers;
using SellPoint.View.Validations;
using SellPoint.View.Services.Pedido.Pedido.Service;
using SellPoint.View.Services.Pedido.Api.Client;
using SellPoint.View.Services.Pedido.Campos.Service;
using SellPoint.View.Services.Pedido;

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

            var baseUrl = configuration["ApiSettings:PedidoBaseUrl"]
                ?? throw new InvalidOperationException("Falta la ruta PedidoBaseUrl en appsettings.json");

            // API Clients
            services.AddHttpClient<IPedidoApiClient, PedidoApiClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient<IPedidoOpcionesService, PedidoOpcionesService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            // Servicios principales
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IPedidoDtoFactory, PedidoDtoFactory>();
            services.AddScoped<IPedidoFormMapper, PedidoFormMapper>();
            services.AddScoped<IPedidoValidator, PedidoValidator>();
            services.AddScoped<IPedidoViewModelMapper, PedidoViewModelMapper>();
            services.AddScoped<IPedidoCamposService, PedidoCamposService>();

            // Formulario
            services.AddScoped<PedidoForm>();

            return services;
        }
    }
}
