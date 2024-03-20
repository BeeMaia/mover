using Microsoft.Extensions.Options;
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

    public async Task<List<Activity>> GetAsync(string userId, CancellationToken cancellationToken) =>
        await activitiesCollection
            .Find(FilterByUserId(userId))
            .Sort(Builders<Activity>.Sort.Descending(_ => _.Timestamp))
            .ToListAsync(cancellationToken);

    public async Task<Activity?> GetByIdRawAsync(string idRaw, CancellationToken cancellationToken) =>
        await activitiesCollection.Find(FilterByIdRaw(idRaw)).FirstOrDefaultAsync(cancellationToken);

    public async Task CreateAsync(Activity newActivity, CancellationToken cancellationToken) =>
        await activitiesCollection.InsertOneAsync(newActivity, cancellationToken: cancellationToken);

    public async Task UpdateAsync(string idRaw, Activity updatedActivity, CancellationToken cancellationToken) =>
        await activitiesCollection.ReplaceOneAsync(FilterByIdRaw(idRaw), updatedActivity, cancellationToken: cancellationToken);

    public async Task RemoveAsync(string idRaw, CancellationToken cancellationToken) =>
        await activitiesCollection.DeleteOneAsync(FilterByIdRaw(idRaw), cancellationToken);

    private static FilterDefinition<Activity> FilterByIdRaw(string idRaw) =>
        Builders<Activity>.Filter.Eq(_ => _.IdRaw, idRaw);

    private static FilterDefinition<Activity> FilterByUserId(string userId) =>
       Builders<Activity>.Filter.Eq(_ => _.UserId, userId);
}
