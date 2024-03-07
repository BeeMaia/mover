using Grpc.Net.Client;
using Mover.Shared.Interfaces;

namespace Mover.FitDecoder.Modules;

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
        builder.Services.AddDaprClient(builder =>
        {
            builder.UseGrpcChannelOptions(new GrpcChannelOptions()
            {
                MaxReceiveMessageSize = 8 * 1024 * 1024,
                MaxSendMessageSize = 8 * 1024 * 1024
            });
        });
    }
}
