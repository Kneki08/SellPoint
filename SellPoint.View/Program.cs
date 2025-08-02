using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SellPoint.View.Forms;
using SellPoint.View.Service.ServiceApiCarrito;
using SellPoint.View.Service.ServiceCarrito;
using SellPoint.View.Service.ServiceApiProducto;
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

            // Puedes cambiar aquí el formulario que quieres abrir:
            var form = host.Services.GetRequiredService<FormProducto>();
            Application.Run(form);
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // JSON options globales
                    services.AddSingleton(new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // Cliente HTTP
                    services.AddHttpClient();

                    // Carrito
                    services.AddScoped<ICarritoApiClient, CarritoApiClient>();
                    services.AddScoped<ICarritoService, CarritoService>();
                    services.AddScoped<FormCarrito>();

                    // Producto
                    services.AddScoped<IProductoApiClient, ProductoApiClient>();
                    services.AddScoped<IProductoService, ProductoService>();
                    services.AddScoped<FormProducto>();
                });
    }
}

