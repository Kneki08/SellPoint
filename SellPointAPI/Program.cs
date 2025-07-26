using Microsoft.OpenApi.Models;
using SellPoint.IOC.Dependencies;
using SellPoint.Persistence.Conext;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace SellPoint.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SellPointContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SellPointConnection")
                ));

            // Swagger
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

            builder.Services.AddCarritoDependency();
            builder.Services.AddProductoDependency();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Middleware de Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SellPoint API v1");
                    c.DisplayRequestDuration(); // Muestra duración de las peticiones
                    c.EnableDeepLinking(); // Permite enlaces directos a secciones
                    c.DefaultModelsExpandDepth(-1);
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
