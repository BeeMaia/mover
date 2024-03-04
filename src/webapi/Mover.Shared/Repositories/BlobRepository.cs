using Dapr.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Shared.Repositories;

public class BlobRepository : IBlobRepository
{
    private readonly DaprClient dapr;
    private readonly ILogger logger;
    private readonly BlobOptions options;

    public BlobRepository(DaprClient dapr, ILoggerFactory loggerFactory, IOptions<BlobOptions> options)
    {
        this.dapr = dapr;
        logger = loggerFactory.CreateLogger(GetType());
        this.options = options.Value;
    }

    public async Task CreateBlobAsync(string blobStorage, string fileName, byte[] data, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating blob: {fileName}", fileName);

        var metadata = new Dictionary<string, string>
        {
            { options.MetadataKey, fileName }
        };

        await dapr.InvokeBindingAsync(blobStorage, "create", data, metadata, cancellationToken);
    }

    public async Task<byte[]> GetBlobAsync(string blobStorage, string fileName, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get blob: {fileName}", fileName);
        var request = new BindingRequest(blobStorage, "get");
        request.Metadata.Add(options.MetadataKey, fileName);

        var response = await dapr.InvokeBindingAsync(request, cancellationToken: cancellationToken);

        return response.Data.ToArray();
    }
}
