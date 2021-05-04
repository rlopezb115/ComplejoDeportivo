using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using servicio.api.ADO.conexion;
using servicio.api.ADO.contrato;
using servicio.api.ADO.servicio;
using servicio.api.Helper;
using servicio.api.Mapping;
using servicio.api.negocio.contrato;
using servicio.api.negocio.servicio;

namespace servicio.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // HATEOAS
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                    .AddScoped<IUrlHelper>(x => x.GetRequiredService<IUrlHelperFactory>()
                                                 .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>()
                                                 .ActionContext));

            services.AddScoped<HATEOASComplejoDeportivoFilterAttribute>();
            services.AddScoped<GeneradorEnlacesComplejoDeportivo>();

            // AutoMapper
            services.AddAutoMapper(typeof(SQLServerProfile));

            // Conexiones
            services.AddScoped<IConexionSQLServer>(sp =>
                new ConexionSQLServer(Configuration.GetConnectionString("CDDB001")));

            // Repositorios
            services.AddScoped<IRepositorioComplejo, RepositorioComplejo>();
            services.AddScoped<IRepositorioJefe, RepositorioJefe>();
            services.AddScoped<IRepositorioSede, RepositorioSede>();
            services.AddScoped<IRepositorioTipoComplejo, RepositorioTipoComplejo>();

            // Servicios
            services.AddScoped<INegocioComplejo, NegocioComplejo>();
            services.AddScoped<INegocioJefe, NegocioJefe>();
            services.AddScoped<INegocioSede, NegocioSede>();
            services.AddScoped<INegocioTipoComplejo, NegocioTipoComplejo>();

            // CORS
            services.AddCors(options => {
                options.AddPolicy(name: "PolicyCORSRegular", 
                                  builder => {
                                      builder.WithOrigins("http://localhost:5000")
                                             .WithMethods("GET", "POST", "PUT", "DELETE")
                                             .WithHeaders("*");
                                  });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}