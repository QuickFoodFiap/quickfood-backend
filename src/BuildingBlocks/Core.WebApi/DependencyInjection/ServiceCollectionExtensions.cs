using Core.WebApi.Configurations;
using Core.WebApi.GlobalErrorMiddleware;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.WebApi.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApiDefautConfig(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(delegate (JsonOptions options)
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            });

            services.AddSwaggerConfig();

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15);
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.SetApiMaxActiveRequests(1);

                opt.AddHealthCheckEndpoint("API QuickFood", "/health");
            }).AddInMemoryStorage();
        }

        public static void UseApiDefautConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApplicationErrorMiddleware();

            app.UseSwaggerConfig(env);

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            })
                .UseHealthChecksUI(options => { options.UIPath = "/health-ui"; });
            ;

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}