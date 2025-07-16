using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Services.CuponService;
using SellPoint.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.IOC1.Dependencies
{
    public static class CuponDependency
    {
        public static void AddCuponDependency(this IServiceCollection service)
        {
            service.AddScoped<ICuponRepository>(sp =>
                new CuponRepository(
                    sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"),
                    sp.GetRequiredService<ILogger<CuponRepository>>()));

            service.AddScoped<ICuponService, CuponService>();
        }
    }
}
