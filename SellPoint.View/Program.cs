using Microsoft.Extensions.DependencyInjection;
using SellPoint.View.Extensions;
using System.Runtime.Versioning;
using Microsoft.Extensions.Configuration;

namespace SellPoint.View
{
    [SupportedOSPlatform("windows")]
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Leer configuración desde appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();

            // Configurar los servicios
            var services = new ServiceCollection();
            ServicesExtensions.Configure(services, apiSettings);

            // Construir ServiceProvider
            var provider = services.BuildServiceProvider();

            
            var opcion = MessageBox.Show(
                "¿Deseas abrir el formulario de Categorías?\nSí: Categorías\nNo: Cupones",
                "Seleccionar módulo",
                MessageBoxButtons.YesNo
            );

            Form form = opcion == DialogResult.Yes
                ? provider.GetRequiredService<FormCategoria>()
                : provider.GetRequiredService<FormCupon>();

            System.Windows.Forms.Application.Run(form);
        }
    }
}








