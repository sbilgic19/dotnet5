using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

// Bir paket eklenince onu kullanabilmek için her zaman StartUp içerisinde bir değişiklik yapılması gerekiyor.
// ConfigureServices 'e service olarak eklenmeli.
namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Uygulama içerisinde kullanılacak servislerin inject edildiği yer

        // Dependencyler burada veriliyor
        public void ConfigureServices(IServiceCollection services) // IServiceCollection services container nesnesidir
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
            // db bizim için bir servis o yüzden burada tanıtılması gerekiyor.
            services.AddDbContext<BookStoreDBContext>(options => options.UseInMemoryDatabase(databaseName:"BookStoreDB")) ;
            services.AddScoped<IBookStoreDBContext>(provider => provider.GetService<BookStoreDBContext>()); // Request lifetime, response dönene kadar
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<ILoggerService, ConsoleLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            // Use'lar middleware, sırası önemli
            // Request ile response arasında gerçekleştirilir.
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
