using Microsoft.Extensions.DependencyInjection;
using SellPoint.View.Forms;
using SellPoint.View.Service.ServiceApiCarrito;
using SellPoint.View.Service.ServiceApiProducto;
using SellPoint.View.Service.ServiceCarrito;
using SellPoint.View.Service.ServiceProducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SellPoint.View.Extensions
{
    public class ServicesExtensions
    {
        public static void Configure(IServiceCollection services)
        {
            // JSON Global
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            services.AddHttpClient();

            // Carrito
            services.AddScoped<ICarritoApiClient, CarritoApiClient>();
            services.AddScoped<ICarritoService, CarritoService>();
            services.AddScoped<FormCarrito>();

            // Producto
            services.AddScoped<IProductoApiClient, ProductoApiClient>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<FormProducto>();
        }
    }
}
}
