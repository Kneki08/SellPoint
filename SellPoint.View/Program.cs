using Microsoft.Extensions.DependencyInjection;
using SellPoint.View.Mappers.Categoria;
using SellPoint.View.Mappers.Cupon;
using SellPoint.View.Services;
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

            // Configurar los servicios
            var services = new ServiceCollection();

            // Configurar opciones JSON
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            services.AddSingleton(jsonOptions);

            // Configurar HttpClient
            services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5271/api/")
            });

            // Registrar HttpServiceBase (base de servicios API)
            services.AddSingleton<HttpServiceBase>();

            // Mappers
            services.AddSingleton<ICategoriaMapper, CategoriaMapper>();
            services.AddSingleton<ICuponMapper, CuponMapper>();

            // Servicios API
            services.AddSingleton<ICategoriaApiClient, CategoriaApiClient>();
            services.AddSingleton<ICuponApiClient, CuponApiClient>();

            // Formularios
            services.AddTransient<FormCategoria>();
            services.AddTransient<FormCupon>();

            // Construir proveedor
            var provider = services.BuildServiceProvider();

            // Diálogo de selección de formulario
            var opcion = MessageBox.Show(
                "¿Deseas abrir el formulario de Categorías?\nSí: Categorías\nNo: Cupones",
                "Seleccionar módulo",
                MessageBoxButtons.YesNo
            );

            // Abrir el formulario seleccionado
            Form form;
            if (opcion == DialogResult.Yes)
                form = provider.GetRequiredService<FormCategoria>();
            else
                form = provider.GetRequiredService<FormCupon>();

            System.Windows.Forms.Application.Run(form);
        }
    }
}







