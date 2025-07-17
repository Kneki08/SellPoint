using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SellPoint.IOC1.Dependencies;
using SellPoint.Persistence.Context;
using System.Reflection;


namespace SellPoint.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ?? DbContext con MySQL usando Pomelo
            builder.Services.AddDbContext<sellpointContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 36)) // Cambia si tu versión es distinta
                ));

            // ?? Dependencias personalizadas
            builder.Services.AddCategoriaDependency();
            builder.Services.AddCuponDependency();
            // Puedes agregar más: ProductoDependency, PedidoDependency, etc.

            // ?? Swagger + documentación XML
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SellPoint API",
                    Version = "v1",
                    Description = "Documentación de la API de SellPoint",
                    Contact = new OpenApiContact
                    {
                        Name = "Soporte",
                        Email = "soporte@sellpoint.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // ?? API + MVC (Controladores + Vistas)
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // ?? Entorno de desarrollo con Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SellPoint API v1");
                    c.DisplayRequestDuration();
                    c.EnableDeepLinking();
                    c.DefaultModelsExpandDepth(-1);
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // ?? Middleware básico
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // ?? Rutas para controladores API y MVC
            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}