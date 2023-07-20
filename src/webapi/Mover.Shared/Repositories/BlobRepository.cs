using Dapr.Client;
using Microsoft.Extensions.Logging;
using Mover.Shared.Interfaces;

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
        var data = new
        {
            blobName = fileName
        };

        var metadata = new Dictionary<string, string>
        {
            { "blobName", fileName }
        };
      
        var obj = await dapr.InvokeBindingAsync<object, object>(blobStorage, "get", data, metadata, cancellationToken: cancellationToken);
        logger.LogInformation(obj.ToString());

        throw new Exception(obj.GetType().ToString());
    }
}
