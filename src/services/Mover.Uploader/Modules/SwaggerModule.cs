﻿using System.Reflection;
using Microsoft.OpenApi.Models;
using Mover.Shared.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mover.Uploader.Modules;

public sealed class SwaggerModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(SetSwaggerGenOptions);
    }

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
        // no dispatchers
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // no endpoints
    }

    private void SetSwaggerGenOptions(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Mover Uploader Api",
            Version = "v1"
        });

        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();

        ConfigureXmlComments(options);
    }

    private void ConfigureXmlComments(SwaggerGenOptions options)
    {
        var xmlFile = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            $"{GetType().Assembly.GetName().Name}.xml");

        // Tells swagger to pick up the output XML document file
        if (!File.Exists(xmlFile))
            return;

        var currentAssembly = Assembly.GetExecutingAssembly();
        options.IncludeXmlComments(xmlFile);

        // Collect all referenced projects output XML document file paths
        var xmlDocs = currentAssembly.GetReferencedAssemblies()
            .Union(new[] { currentAssembly.GetName() })
            .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location)!, $"{a.Name}.xml"))
            .Where(File.Exists).ToArray();

        Array.ForEach(xmlDocs, (d) => { options.IncludeXmlComments(d); });
    }
}
