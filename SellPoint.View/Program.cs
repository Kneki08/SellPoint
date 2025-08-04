using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SellPoint.View.Extensions;
using SellPoint.View.Forms;
using SellPoint.View.Service.ServiceApiCarrito;
using SellPoint.View.Service.ServiceApiProducto;
using SellPoint.View.Service.ServiceCarrito;
using SellPoint.View.Service.ServiceProducto;
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
                    ServicesExtensions.Configure(services);


                });
    }
}


