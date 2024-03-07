using Mover.Shared.Interfaces;

namespace Mover.Stats.Modules;

public class LoggingModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 0;

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationInsightsTelemetry();
    }
}
