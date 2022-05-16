
using Bernal.ERP.ControleInventario.Dominio.Repositorios;
using Bernal.ERP.ControleInventario.Dominio.Servicos;
using Bernal.ERP.ControleInventario.Infra;
using Bernal.ERP.ControleInventario.Infra.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace Bernal.ERP.ControleInventario.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            
            services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("StringConnect")));
            services.AddControllers();
            services.AddControllers()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                   options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
               });
            services.AddScoped<IInventarioRepositorio, InventarioRepositorio>();
            services.AddScoped<IInventarioServico, InventarioServico>();
           
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/HOME/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=home}/{action=Index}/{id?}");
            });
        }
    }
}
