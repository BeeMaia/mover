using Mover.Shared;
using Mover.Shared.Extensions;
using Mover.Shared.Interfaces;
using Mover.Shared.Models.GPX;
using Mover.Stats.Interfaces;
using Mover.Stats.Shared.Models;

namespace Mover.Stats.Services;

public class StatsService : IStatsService
{
    private readonly IBlobRepository blobRepository;
    private readonly ILogger logger;

    public StatsService(IBlobRepository blobRepository, ILoggerFactory loggerFactory)
    {
        this.blobRepository = blobRepository ?? throw new ArgumentNullException(nameof(blobRepository));
        logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task WriteAsync(Guid rawId, string fileName, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start write stats for file: {fileName}", fileName);

        var data = await blobRepository.GetBlobAsync(Constants.Dapr.MOVER_GPXBLOB, fileName, cancellationToken);
        var gpx = data.ToGpx();
        var points = gpx?.Trk?.FirstOrDefault()?.Trkseg?.FirstOrDefault()?.Trkpt;
        var track = BuildTrackEntity(rawId, fileName, gpx, points);
    }

    private static Track BuildTrackEntity(Guid rawId, string fileName, Gpx? gpx, GpxPoint[]? points)
    {
        var track = new Track()
        {
            IdRaw = rawId,
            FileName = fileName,
            ActivityType = gpx?.Trk?.FirstOrDefault()?.Type ?? string.Empty,
            Timestamp = gpx?.Metadata?.Time.ToEpoch() ?? DateTime.UtcNow.ToEpoch()
        };

        if (points is not null)
        {
            track.Points = new List<Point>();
            foreach (var p in points)
            {
                var point = new Point
                {
                    Timestamp = p.Time.ToEpoch()
                };

                if (p.Extensions is not null)
                {
                    var extension = p.Extensions as GpxTrkTrkptExtensions;

                    point.Cadence = extension?.TrackPointExtension?.Cadence ?? 0;
                    point.HeartRate = extension?.TrackPointExtension?.HeartRate ?? 0;
                    point.Speed = extension?.TrackPointExtension?.Speed ?? 0;
                    point.Temp = extension?.TrackPointExtension?.Temp ?? 0;
                }

                track.Points.Append(point);
            }
        }
        else
        {
            track.Points = Enumerable.Empty<Point>();
        }

        return track;
    }
}
