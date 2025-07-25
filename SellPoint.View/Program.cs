using SellPoint.View;
using SellPoint.View.Services.CategoriaApiClient;
using SellPoint.View.Services.CuponApiClient;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace SellPoint.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Instancia de HttpClient con base URI de la API  
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5271/api/")
            };

            // Inyecci�n de dependencias  
            var categoriaService = new CategoriaApiClient(httpClient);
            var cuponService = new CuponApiClient(httpClient);

            // Selecci�n de formulario a ejecutar  
            var opcion = MessageBox.Show(
                "�Deseas abrir el formulario de Categor�as?\n(S�: Categor�as, No: Cupones)",
                "Seleccionar m�dulo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Elige el formulario seg�n la opci�n  
            Form formulario = opcion == DialogResult.Yes
                ? new FormCategoria(categoriaService)
                : new FormCupon(cuponService);

            // Ejecuta el formulario  
            System.Windows.Forms.Application.Run(formulario);
        }
    }
}


