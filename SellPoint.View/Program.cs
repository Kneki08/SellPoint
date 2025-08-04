using Microsoft.Extensions.DependencyInjection;
using SellPoint.View.Extensions;
using System.Text.Json;

namespace SellPoint.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                var services = new ServiceCollection();
                var config = LoadConfiguration();
                
                services.AddDetallePedidoServices(config.BaseUrl, config.TimeoutSeconds);

                using var provider = services.BuildServiceProvider();
                Application.Run(provider.GetRequiredService<Form1>());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n\nLa aplicación se cerrará.",
                              "Error de Configuración",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private static ApiConfig LoadConfiguration()
        {
            const string configFileName = "appsettings.json";
            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFileName);

            if (!File.Exists(configPath))
            {
                // Crear archivo de configuración por defecto si no existe
                var defaultConfig = new ApiConfig
                {
                    BaseUrl = "http://localhost:5271/api/",
                    TimeoutSeconds = 30
                };

                File.WriteAllText(configPath, JsonSerializer.Serialize(defaultConfig));
                return defaultConfig;
            }

            var json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<ApiConfig>(json) ?? throw new InvalidOperationException("Configuración inválida");

            if (string.IsNullOrWhiteSpace(config.BaseUrl))
                throw new InvalidOperationException("La URL base no está configurada");

            return config;
        }
    }

    public class ApiConfig
    {
        public string BaseUrl { get; set; }
        public int TimeoutSeconds { get; set; }
    }
}
