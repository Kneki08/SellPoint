
using SellPoint.View;
using SellPoint.View.Services.CategoriaApiClient;
using SellPoint.View.Services.CuponApiClient;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace SellPoint.View.Service
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Instancia de HttpClient compartida
            var httpClient = new HttpClient();

            // Inyecci�n de servicios
            var categoriaService = new CategoriaApiClient(httpClient);
            var cuponService = new CuponApiClient(httpClient);

            // Mostrar men� inicial para elegir el formulario
            var opcion = MessageBox.Show(
                "�Deseas abrir el formulario de Categor�as?\n(S�: Categor�as, No: Cupones)",
                "Seleccionar m�dulo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (opcion == DialogResult.Yes)
            {
                Application.Run(new FormCategoria(categoriaService));
            }
            else
            {
                Application.Run(new CuponForm(cuponService));
            }
        }
    }
}
