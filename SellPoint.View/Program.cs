using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SellPoint.View.Factories;
using SellPoint.View.Forms;
using SellPoint.View.Mappers;
using SellPoint.View.Services.Pedido;
using SellPoint.View.Validations;

namespace SellPoint.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();

            // Ejecutar el formulario desde el contenedor de servicios
            var form = host.Services.GetRequiredService<PedidoForm>();
            Application.Run(form);
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var configuration = context.Configuration;

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
                });
    }
}