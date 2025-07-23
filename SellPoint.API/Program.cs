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

            builder.Services.AddDbContext<sellpointContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            
            builder.Services.AddCategoriaDependency();
            builder.Services.AddCuponDependency();
            

            
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

            
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

           
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

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            
            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}