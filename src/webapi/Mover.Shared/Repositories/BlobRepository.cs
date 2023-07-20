using Dapr.Client;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mover.Shared.Interfaces;
using System.ComponentModel;
using System.Text.Json;

namespace Mover.Shared.Repositories;

public class BlobRepository : IBlobRepository
{
    private readonly DaprClient dapr;
    private readonly ILogger logger;

    public BlobRepository(DaprClient dapr, ILoggerFactory loggerFactory)
    {
        this.dapr = dapr;
        logger = loggerFactory.CreateLogger(GetType());
    }

    public Task CreateBlobAsync(string blobStorage, string fileName, byte[] data, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating blob: {fileName}", fileName);

        var metadata = new Dictionary<string, string>
        {
            { "blobName", fileName }
        };

        return dapr.InvokeBindingAsync(blobStorage, "create", data, metadata, cancellationToken);
    }

    public async Task<byte[]> GetBlobAsync(string blobStorage, string fileName, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get blob: {fileName}", fileName);
        var request = new BindingRequest(blobStorage, "get");
        request.Metadata.Add("blobName", fileName);

        var response = await dapr.InvokeBindingAsync(request, cancellationToken: cancellationToken);
        logger.LogInformation(JsonSerializer.Serialize(response));

        throw new Exception(response.GetType().ToString());
    }
}
