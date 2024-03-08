using Dynastream.Fit;
using Mover.FitDecoder.Extensions;
using Mover.FitDecoder.Interfaces;
using Mover.Shared;
using Mover.Shared.Interfaces;
using Mover.Shared.Models.GPX;

namespace Mover.FitDecoder.Services;

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

    public async Task<Gpx> DecodeAsync(Guid rawId, string fileName, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start decoding blob: {fileName}", fileName);

        var data = await blobRepository.GetBlobAsync(Constants.Dapr.MOVER_FITBLOB, fileName, cancellationToken);
        var gpx = ParseFitData(fileName, data);

        logger.LogInformation("Decoded blob: {fileName}", fileName);

        return gpx;
    }

    private Gpx ParseFitData(string fileName, byte[] data)
    {
        System.DateTime startTime = System.DateTime.MinValue;
        var points = new List<GpxPoint>();
        var trk = new GpxTrk
        {
            Type = Sport.Generic.ToString()
        };

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

        mesgBroadcaster.MesgEvent += (sender, e) =>
        {
            if (e.mesg.Num == MesgNum.Session)
            {
                trk.Type = GetActivityType(e.mesg);
                startTime = GetActivityTimestamp(e.mesg);
            }
        };

        fitDecoder.MesgEvent += mesgBroadcaster.OnMesg;

        using var fitStream = new MemoryStream(data);
        var status = fitDecoder.IsFIT(fitStream);
        status &= fitDecoder.CheckIntegrity(fitStream);

        // Process the file
        try
        {
            if (status)
            {
                logger.LogInformation("Decoding...");
                fitDecoder.Read(fitStream);
                logger.LogInformation($"Decoded FIT file ");
            }
            else
            {
                try
                {
                    logger.LogWarning($"Integrity Check Failed {fileName}");
                    if (fitDecoder.InvalidDataSize)
                    {
                        logger.LogInformation("Invalid Size Detected, Attempting to decode...");
                        fitDecoder.Read(fitStream);
                    }
                    else
                    {
                        logger.LogInformation("Attempting to decode by skipping the header...");
                        fitDecoder.Read(fitStream, DecodeMode.InvalidHeader);
                    }
                }
                catch (FitException ex)
                {
                    logger.LogError($"DecodeDemo caught FitException: {ex.Message}");
                    throw;
                }
            }
        }
        finally
        {
            fitStream.Close();
        }

        trk.Trkseg = [new GpxTrkTrkseg { Trkpt = [.. points] }];

        return new Gpx
        {
            Version = (decimal)1.1,
            Creator = "Mover",
            Metadata = new GpxMetadata { Time = startTime },
            Trk =
            [
               trk
            ]
        };
    }

    private static string GetActivityType(Mesg sessionMesg)
    {
        var sport = (SubSport)sessionMesg.FieldValue<byte>(SessionMesg.FieldDefNum.SubSport);
        return sport.ToString();
    }

    private static System.DateTime GetActivityTimestamp(Mesg sessionMesg)
    {
        var timestamp = sessionMesg.FieldValue<uint>(SessionMesg.FieldDefNum.StartTime);
        var datetime = new Dynastream.Fit.DateTime(timestamp);

        return datetime.GetDateTime();
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
