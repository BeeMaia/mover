using Mover.Shared.Interfaces;

namespace Mover.FitDecoder.Modules;

public sealed class CorsModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", corsBuilder =>
                corsBuilder.AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader());
        });
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // no endpoints
    }

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
        // no dispatchers
    }
}
