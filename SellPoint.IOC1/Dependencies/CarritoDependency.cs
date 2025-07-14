using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Interfaces.Servicios;
using SellPoint.Aplication.Services.CarritoService;
using SellPoint.Aplication.Services.CategoriaService.cs;
using SellPoint.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.IOC1.Dependencies
{
    public static class CarritoDependency
    {
        public static void AddCarritoDependency(this IServiceCollection service)
        {
            //service.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
            service.AddScoped<ICarritoRepository>(sp =>
               new CarritoRepository(
        sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"),
        sp.GetRequiredService<ILogger<CarritoRepository>>()));

            service.AddScoped<ICarritoService, CarritoService>();
        }

        
    }
}
