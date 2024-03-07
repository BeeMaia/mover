using Mover.Shared.Interfaces;

namespace Mover.Stats.Modules;

public sealed class AuthenticationModule : IModule
{
    public bool IsEnabled => false;

    public int Order => 0;

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
        // no dispatchers
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // no endpoints
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
    }
}
