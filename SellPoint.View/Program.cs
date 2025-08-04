using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SellPoint.View.Extensions;
using SellPoint.View.Forms;

namespace SellPoint.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();

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
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddPedidoDependencies(context.Configuration);
                });
    }
}