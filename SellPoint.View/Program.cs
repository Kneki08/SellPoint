using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SellPoint.View.Extensions;
using SellPoint.View.Forms;
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

            var result = MessageBox.Show(
                "¿Quieres abrir el módulo de Producto?",
                "Seleccionar módulo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                var formProducto = host.Services.GetRequiredService<FormProducto>();
                Application.Run(formProducto);
            }
            else
            {
                var formCarrito = host.Services.GetRequiredService<FormCarrito>();
                Application.Run(formCarrito);
            }
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


