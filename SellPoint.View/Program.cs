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

            // HttpClient compartido
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5271/api/")
            };

            // Crear instancias de los mapeadores
            var categoriaMapper = new CategoriaMapper();
            var cuponMapper = new CuponMapper();

            // Opciones JSON centralizadas
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

            // Servicios con sus dependencias
            var categoriaService = new CategoriaApiClient(httpClient, categoriaMapper, jsonOptions);
            var cuponService = new CuponApiClient(httpClient, cuponMapper, jsonOptions);

            // Diálogo para elegir el módulo
            var opcion = MessageBox.Show(
                "¿Deseas abrir el formulario de Categorías?\n(Sí: Categorías, No: Cupones)",
                "Seleccionar módulo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Ejecutar el formulario correspondiente
            System.Windows.Forms.Application.Run(opcion == DialogResult.Yes
                ? new FormCategoria(categoriaService)
                : new FormCupon(cuponService));
        }
    }
}



