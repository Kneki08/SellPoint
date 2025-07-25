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

           
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5271/api/")
            };

            
            var categoriaService = new CategoriaApiClient(httpClient);
            var cuponService = new CuponApiClient(httpClient);

            
            var opcion = MessageBox.Show(
                "¿Deseas abrir el formulario de Categorías?\n(Sí: Categorías, No: Cupones)",
                "Seleccionar módulo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            
            Form formulario = opcion switch
            {
                DialogResult.Yes => new FormCategoria(categoriaService),
                DialogResult.No => new FormCupon(cuponService),
                _ => null
            };

            if (formulario != null)
                Application.Run(formulario);
        }
    }
}

