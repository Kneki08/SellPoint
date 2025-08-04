using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SellPoint.View.Service.ServiceProducto;
using SellPoint.View.Forms;
using SellPoint.View.Service.ServiceApiCarrito;
using SellPoint.View.Service.ServiceCarrito;
using SellPoint.View.Service.ServiceApiProducto;
//using SellPoint.View.Service.ServiceProducto;
using System.Text.Json;

namespace SellPoint.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var host = CreateHostBuilder().Build();

            // Cambia aquí el formulario a lanzar (Producto o Carrito)
            var form = host.Services.GetRequiredService<FormCarrito>();
            Application.Run(form);
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // JSON Global
                    services.AddSingleton(new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    services.AddHttpClient();

                    // Configuración de servicios del módulo Carrito
                    services.AddScoped<ICarritoApiClient, CarritoApiClient>();
                    services.AddScoped<ICarritoService, CarritoService>();
                    services.AddScoped<FormCarrito>();

                    // Configuración de servicios del módulo Producto
                    services.AddScoped<IProductoApiClient, ProductoApiClient>();
                    services.AddScoped<IProductoService, ProductoService>();
                    services.AddScoped<FormProducto>();
                });
    }
}


