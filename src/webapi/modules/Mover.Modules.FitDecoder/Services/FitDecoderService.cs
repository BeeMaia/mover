using Dynastream.Fit;
using Microsoft.Extensions.Logging;
using Mover.Modules.FitDecoder.Extensions;
using Mover.Modules.FitDecoder.Interfaces;
using Mover.Shared;
using Mover.Shared.Extensions;
using Mover.Shared.Interfaces;
using Mover.Shared.Models.GPX;

namespace Mover.Modules.FitDecoder.Services;

public class FitDecoderService : IFitDecoderService
{
    private const int GarminDegreeDividend = 11930465;
    private readonly IBlobRepository blobRepository;
    private readonly ILogger logger;

    public FitDecoderService(IBlobRepository blobRepository, ILoggerFactory loggerFactory)
    {
        this.blobRepository = blobRepository ?? throw new ArgumentNullException(nameof(blobRepository));
        logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task<string> DecodeAsync(Guid rawId, string fileName, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start decoding blob: {fileName}", fileName);

        var data = await blobRepository.GetBlobAsync(Constants.Dapr.MOVER_FITBLOB, fileName, cancellationToken).ConfigureAwait(false);
        var gpx = DecodeAsGPX(data);

        logger.LogInformation("Decoded blob: {fileName}", fileName);

        var gpxFileName = $"{Path.GetFileNameWithoutExtension(fileName)}.gpx";
        await blobRepository.CreateBlobAsync(Constants.Dapr.MOVER_GPXBLOB, gpxFileName, gpx.ToArray(), cancellationToken).ConfigureAwait(false);
        return gpxFileName;
    }

    private static Gpx DecodeAsGPX(byte[] data)
    {
        var points = ParseFitData(data);

        return new Gpx
        {
            Version = (decimal)1.1,
            Creator = "Mover",
            Trk = 
            [
                new GpxTrk
                {
                    Trkseg =
                    [
                        new GpxTrkTrkseg { Trkpt = [.. points] }
                    ]
                }
            ]
        };
    }

    private static List<GpxPoint> ParseFitData(byte[] data)
    {
        var points = new List<GpxPoint>();

        var fitDecoder = new Decode();
        var mesgBroadcaster = new MesgBroadcaster();

        mesgBroadcaster.RecordMesgEvent += (sender, e) =>
        {
            if (e.mesg.Num == MesgNum.Record)
            {
                var trkPoint = CreateTrkPointFromRecord((RecordMesg)e.mesg);
                if (trkPoint.Lat != 0 && trkPoint.Lon != 0)
                    points.Add(trkPoint);
            }
        };

        mesgBroadcaster.MesgEvent += (sender, e) => { };

        fitDecoder.MesgEvent += mesgBroadcaster.OnMesg;

        using (var fitStream = new MemoryStream(data))
        {
            fitDecoder.Read(fitStream);
        }

        return points;
    }

    private static GpxPoint CreateTrkPointFromRecord(RecordMesg recordMessage)
    {
        var time = recordMessage.FieldValue<uint>(RecordMesg.FieldDefNum.Timestamp);
        var trkPoint = new GpxPoint
        {
            Time = new Dynastream.Fit.DateTime(time).GetDateTime(),
            Lat = recordMessage.FieldValue<decimal>(RecordMesg.FieldDefNum.PositionLat) / GarminDegreeDividend,
            Lon = recordMessage.FieldValue<decimal>(RecordMesg.FieldDefNum.PositionLong) / GarminDegreeDividend,
            Ele = recordMessage.FieldValue<decimal>(RecordMesg.FieldDefNum.EnhancedAltitude),
            Extensions = new GpxTrkTrkptExtensions
            {
                TrackPointExtension = new TrackPointExtension
                {
                    Temp = recordMessage.FieldValue<decimal>(RecordMesg.FieldDefNum.Temperature),
                    Cadence = recordMessage.FieldValue<short>(RecordMesg.FieldDefNum.Cadence),
                    HeartRate = recordMessage.FieldValue<short>(RecordMesg.FieldDefNum.HeartRate),
                    Speed = recordMessage.FieldValue<short>(RecordMesg.FieldDefNum.EnhancedSpeed)
                }
            }
        };

        return trkPoint;
    }
}
