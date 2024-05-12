﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace Core.WebApi.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services) => services.AddSwaggerGen(c =>
                                                                                          {
                                                                                              c.SwaggerDoc("v1", new OpenApiInfo { Title = "API QuickFood", Version = "v1" });
                                                                                          });

        public static void UseSwaggerConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}