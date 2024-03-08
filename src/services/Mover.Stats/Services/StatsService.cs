using Mover.Shared;
using Mover.Shared.Extensions;
using Mover.Shared.Interfaces;
using Mover.Shared.Models.GPX;
using Mover.Stats.Interfaces;
using Mover.Stats.Repositories;
using Mover.Stats.Shared.Models;

namespace Mover.Stats.Services;

public class StatsService : IStatsService
{
    private readonly IBlobRepository blobRepository;
    private readonly ActivityRepository activityRepository;
    private readonly ILogger logger;

    public StatsService(IBlobRepository blobRepository, ILoggerFactory loggerFactory, ActivityRepository activityRepository)
    {
        this.blobRepository = blobRepository ?? throw new ArgumentNullException(nameof(blobRepository));
        this.activityRepository = activityRepository;
        logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task WriteAsync(Guid rawId, string fileName, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start write stats for file: {fileName}", fileName);

        var data = await blobRepository.GetBlobAsync(Constants.Dapr.MOVER_GPXBLOB, fileName, cancellationToken);
        var gpx = data.ToGpx();
        var points = gpx?.Trk?.FirstOrDefault()?.Trkseg?.FirstOrDefault()?.Trkpt;
        var track = BuildTrackEntity(rawId, fileName, gpx, points);

        await activityRepository.CreateAsync(track);
    }

    private static Activity BuildTrackEntity(Guid rawId, string fileName, Gpx? gpx, GpxPoint[]? points)
    {
        var track = new Activity()
        {
            IdRaw = rawId.ToString(),
            FileName = fileName,
            ActivityType = gpx?.Trk?.FirstOrDefault()?.Type ?? string.Empty,
            Timestamp = gpx?.Metadata?.Time.ToEpoch() ?? DateTime.UtcNow.ToEpoch()
        };

        if (points is not null)
        {
            var trkPoints = new List<Point>();
            track.TotalTime = points.Last().Time.ToEpoch() - points.First().Time.ToEpoch();
            var coordinates = new List<(double, double)>();
            var prevElevation = points.First().Ele;
            var index = 0;
            foreach (var p in points)
            {
                prevElevation = CalculateTotalPositiveDrop(track, prevElevation, index, p);
                index++;

                coordinates.Add((Convert.ToDouble(p.Lat), Convert.ToDouble(p.Lon)));
                var point = CreatePoint(p);

                trkPoints.Add(point);
            }

            track.Points = trkPoints;
            track.TotalDistance = CalculateTotalDistance(coordinates);
        }
        else
        {
            track.Points = Enumerable.Empty<Point>();
        }

        return track;
    }

    private static decimal CalculateTotalPositiveDrop(Activity track, decimal prevElevation, int index, GpxPoint p)
    {
        if (index != 0)
        {
            var elevationChange = Convert.ToDouble(p.Ele - prevElevation);

            if (elevationChange > 0)
            {
                track.TotalPositiveDrop += elevationChange;
            }

            prevElevation = p.Ele;
        }

        return prevElevation;
    }

    private static Point CreatePoint(GpxPoint p)
    {
        var point = new Point
        {
            Timestamp = p.Time.ToEpoch()
        };

        if (p.Extensions is not null)
        {
            var extension = p.Extensions as GpxTrkTrkptExtensions;

            point.Elevation = p.Ele;
            point.Cadence = extension?.TrackPointExtension?.Cadence ?? 0;
            point.HeartRate = extension?.TrackPointExtension?.HeartRate ?? 0;
            point.Speed = extension?.TrackPointExtension?.Speed ?? 0;
            point.Temp = extension?.TrackPointExtension?.Temp ?? 0;
        }

        return point;
    }

    private static double CalculateTotalDistance(List<(double, double)> coordinates)
    {
        var totalDistance = 0.0;

        for (var i = 0; i < coordinates.Count - 1; i++)
        {
            var lat1 = coordinates[i].Item1;
            var lon1 = coordinates[i].Item2;
            var lat2 = coordinates[i + 1].Item1;
            var lon2 = coordinates[i + 1].Item2;

            var distance = CalculateDistance(lat1, lon1, lat2, lon2);
            totalDistance += distance;
        }

        return totalDistance;
    }

    private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371; // Radius of the Earth in kilometers

        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        var distance = R * c;
        return distance;
    }

    private static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}
