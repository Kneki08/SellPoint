using SellPoint.View.Service.ServiceProducto;
using SellPoint.View.Services.ProductoApiClient;

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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Create an instance of HttpClient to pass as a parameter to ProductoApiClient
            HttpClient httpClient = new HttpClient();

            // Pass the HttpClient instance to the ProductoApiClient constructor
            IProductoApiClient productoApiClient = new ProductoApiClient(httpClient);

            Application.Run(new FormProducto(productoApiClient));
        }
    }
}