using Microsoft.Extensions.DependencyInjection;
using SellPoint.View.Mappers.Cupon;
using SellPoint.View.Services.CategoriaApiClient;
using SellPoint.View.Services.CuponApiClient;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Windows.Forms;

namespace SellPoint.View
{
    [SupportedOSPlatform("windows")]
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            
            var services = new ServiceCollection();

            
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            services.AddSingleton(jsonOptions);

            
            services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5271/api/")
            });

            
            services.AddSingleton<ICategoriaMapper, CategoriaMapper>();
            services.AddSingleton<ICuponMapper, CuponMapper>();

            
            services.AddSingleton<ICategoriaApiClient, CategoriaApiClient>();
            services.AddSingleton<ICuponApiClient, CuponApiClient>();

            
            services.AddTransient<FormCategoria>();
            services.AddTransient<FormCupon>();

            
            var provider = services.BuildServiceProvider();

           
            var opcion = MessageBox.Show(
                "¿Deseas abrir el formulario de Categorías?\n(Sí: Categorías, No: Cupones)",
                "Seleccionar módulo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            System.Windows.Forms.Application.Run(opcion == DialogResult.Yes
                ? provider.GetRequiredService<FormCategoria>()
                : provider.GetRequiredService<FormCupon>());
        }
    }
}




