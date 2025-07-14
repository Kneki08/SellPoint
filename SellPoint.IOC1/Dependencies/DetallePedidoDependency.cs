using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Interfaces.Servicios;
using SellPoint.Aplication.Services.DetallepedidoService;
using SellPoint.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SellPoint.IOC1.Dependencies
{
    public static class DetallePedidoDependency
    {
        public static void AddDetalleDependency(this IServiceCollection service)
        {
            //service.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
            service.AddScoped<IDetallePedidoRepository>(sp =>
               new DetallePedidoRepository(
        sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"),
        sp.GetRequiredService<ILogger<DetallePedidoRepository>>()));

           service.AddScoped<IDetallepedidoService, DetallepedidoService>();
        }

    }
}
