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

    public async Task<List<Activity>> GetAsync() =>
        await activitiesCollection.Find(_ => true).ToListAsync();

    public async Task<Activity?> GetAsync(string idRaw) =>
        await activitiesCollection.Find(x => x.IdRaw == idRaw).FirstOrDefaultAsync();

    public async Task CreateAsync(Activity newActivity) =>
        await activitiesCollection.InsertOneAsync(newActivity);

    public async Task UpdateAsync(string idRaw, Activity updatedActivity) =>
        await activitiesCollection.ReplaceOneAsync(x => x.IdRaw == idRaw, updatedActivity);

    public async Task RemoveAsync(string idRaw) =>
        await activitiesCollection.DeleteOneAsync(x => x.IdRaw == idRaw);
}
