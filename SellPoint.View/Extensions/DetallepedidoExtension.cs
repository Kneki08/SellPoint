using Microsoft.Extensions.DependencyInjection;
using SellPoint.View.HTTP;
using SellPoint.View.Service;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SellPoint.View.Extensions
{
    public static class DetallepedidoExtension
    {

        public static IServiceCollection AddDetallePedidoServices(this IServiceCollection services, string baseUrl, int timeoutSeconds = 30)
        {
            // Versión simplificada sin IConfiguration
            services.AddHttpClient<IHttpApiClient, HttpApiClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddScoped<IDetallePedidoService, DetallePedidoService>();
            services.AddTransient<Form1>();

            return services;
        }
    } 
}
