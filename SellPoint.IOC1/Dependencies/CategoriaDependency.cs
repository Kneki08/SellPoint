using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Services.CategoriaService.cs;
using SellPoint.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.IOC1.Dependencies
{
    public static class CategoriaDependency
    {
        public static void AddCategoriaDependency(this IServiceCollection service)
        {
            
            service.AddScoped<ICategoriaRepository>(sp =>
    new CategoriaRepository(
        sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"),
        sp.GetRequiredService<ILogger<CategoriaRepository>>()));

            service.AddScoped<ICategoriaService, CategoriaService>();
        }

    }
}
