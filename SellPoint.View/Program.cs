using Microsoft.Extensions.Configuration;
using SellPoint.View.Settings;
using SellPoint.View.Services.Pedido;
using SellPoint.View.Forms;

namespace SellPoint.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Carga configuración
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();

            // Inyección de dependencias
            var pedidoService = new PedidoService(
                apiSettings!.PedidoBaseUrl ?? throw new InvalidOperationException("Falta la ruta PedidoBaseUrl en appsettings.json")
            );
            Application.Run(new PedidoForm(pedidoService));
        }
    }
}