using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Mover.Shared.Interfaces;

public interface IModule
{
    bool IsEnabled { get; }
    int Order { get; }

    void RegisterModule(WebApplicationBuilder builder);
    void MapEndpoints(IEndpointRouteBuilder endpoints);
    void MapDispatchers(IEndpointRouteBuilder endpoints);
}
