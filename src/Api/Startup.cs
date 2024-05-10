using Application.UseCases;
using HealthChecks.UI.Client;
using Infra.Context;
using Infra.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration
                       , IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API QuickFood", Version = "v1" });
            });

            services.AddHealthChecks();
            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15);
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.SetApiMaxActiveRequests(1);

                opt.AddHealthCheckEndpoint("API QuickFood", "/health");
            }).AddInMemoryStorage();

            services.AddInfraDependencyServices(Configuration.GetConnectionString("DefaultConnection"));

            services.AddTransient<IUsuarioUseCase, UsuarioUseCase>();
            services.AddTransient<IProdutoUseCase, ProdutoUseCase>();
        }

        public void Configure(IApplicationBuilder app, ApplicationDbContext context)
        {
            DatabaseMigrator.MigrateDatabase(context);

            if (_environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            #region Healthcheck
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            })
                .UseHealthChecksUI(options => { options.UIPath = "/health-ui"; }); ;
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public static class DatabaseMigrator
    {
        public static void MigrateDatabase(ApplicationDbContext context)
        {
            var migrator = context.GetService<IMigrator>();

            migrator.Migrate();
        }
    }
}