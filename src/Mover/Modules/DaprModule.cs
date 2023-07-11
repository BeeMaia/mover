using Mover.Interfaces;

namespace Mover.Modules;

public sealed class DaprModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 0;

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // no endpoints
    }

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
        // no dispatchers
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddDaprClient();
    }
}
