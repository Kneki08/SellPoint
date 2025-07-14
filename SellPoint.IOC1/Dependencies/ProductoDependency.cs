using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Services.PedidoService;
using SellPoint.Aplication.Services.ProductoService;
using SellPoint.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.IOC1.Dependencies
{
     public  static class ProductoDependency
    {
        public static void AddProductoDependency(this IServiceCollection service)
        {
            //service.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
            service.AddScoped<IProductoRepository>(sp =>
               new ProductoRepository(
        sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"),
        sp.GetRequiredService<ILogger<ProductoRepository>>()));

            service.AddScoped<IProductoService, ProductoService>();
        }
    }
}
