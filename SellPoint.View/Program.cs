using Microsoft.Extensions.DependencyInjection;
using SellPoint.View.Service.DetallePedidoClient.Contract;
using SellPoint.View.Service.DetallePedidoClient.Implement;
using SellPoint.View.Repositories;


namespace SellPoint.View
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Configurar el servicio
            var services = new ServiceCollection();

            // Configurar HttpClient
            services.AddHttpClient<IDetallePedidoApiClient, DetallePedidoApiClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5271/");
                client.Timeout = TimeSpan.FromSeconds(105);
            });

            // Registrar servicios
            services.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
            services.AddTransient<Form1>();

            // Construir el proveedor
            using var serviceProvider = services.BuildServiceProvider();

            // Ejecutar la aplicación
            var mainForm = serviceProvider.GetRequiredService<Form1>();
            Application.Run(mainForm);
        }

        
    }
}
