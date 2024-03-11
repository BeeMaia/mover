using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Mover.Shared.Models;
using Mover.Stats.Shared.Models;

namespace Mover.Stats.Repositories;

public class ActivityRepository
{
    private readonly IMongoCollection<Activity> activitiesCollection;

    public ActivityRepository(IMongoClient mongoClient, IOptions<MongoDbOptions> options)
    {
        var mongoDatabase = mongoClient.GetDatabase(
            options.Value.DatabaseName);

        activitiesCollection = mongoDatabase.GetCollection<Activity>(
            options.Value.ActivitiesCollectionName);
    }

    public async Task<List<Activity>> GetAsync(CancellationToken cancellationToken) =>
        await activitiesCollection.Find(_ => true).SortByDescending(_=> _.Timestamp).ToListAsync(cancellationToken);

    public async Task<Activity?> GetAsync(string idRaw, CancellationToken cancellationToken) =>
        await activitiesCollection.Find(x => x.IdRaw == idRaw).FirstOrDefaultAsync(cancellationToken);

    public async Task CreateAsync(Activity newActivity, CancellationToken cancellationToken) =>
        await activitiesCollection.InsertOneAsync(newActivity, cancellationToken: cancellationToken);

    public async Task UpdateAsync(string idRaw, Activity updatedActivity, CancellationToken cancellationToken) =>
        await activitiesCollection.ReplaceOneAsync(x => x.IdRaw == idRaw, updatedActivity, cancellationToken: cancellationToken);

    public async Task RemoveAsync(string idRaw, CancellationToken cancellationToken) =>
        await activitiesCollection.DeleteOneAsync(x => x.IdRaw == idRaw, cancellationToken);
}
