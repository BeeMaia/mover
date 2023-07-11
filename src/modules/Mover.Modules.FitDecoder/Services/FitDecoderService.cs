using Dynastream.Fit;
using Mover.Modules.FitDecoder.Extensions;
using Mover.Modules.FitDecoder.Interfaces;
using Mover.Shared;
using Mover.Shared.Extensions;
using Mover.Shared.Interfaces;
using Mover.Shared.Models.GPX;

namespace Mover.Modules.FitDecoder.Services;

public class FitDecoderService : IFitDecoderService
{
    private readonly IBlobRepository blobRepository;

    public FitDecoderService(IBlobRepository blobRepository)
    {
        this.blobRepository = blobRepository ?? throw new ArgumentNullException(nameof(blobRepository));
    }

    public async Task DecodeAsync(Guid rawId, string fileName, CancellationToken cancellationToken)
    {
        var data = await blobRepository.GetBlobAsync(Constants.Dapr.MOVER_FITBLOB, fileName, cancellationToken).ConfigureAwait(false);
        var gpx = DecodeAsGPX(data);
        await blobRepository.CreateBlobAsync(Constants.Dapr.MOVER_GPXBLOB, $"{Path.GetFileNameWithoutExtension(fileName)}.gpx", gpx.ToArray(), cancellationToken).ConfigureAwait(false);
    }

    private static Gpx DecodeAsGPX(byte[] data)
    {
        var points = new List<GpxTrkTrksegTrkpt>();

        var fitDecoder = new Decode();
        var mesgBroadcaster = new MesgBroadcaster();

        mesgBroadcaster.RecordMesgEvent += (sender, e) =>
        {
            if (e.mesg.Num == MesgNum.Record)
            {
                // Access individual fields from the Record message
                var recordMessage = (RecordMesg)e.mesg;
                // Extract necessary information and write to GPX file
                // e.g., latitude, longitude, time, etc.
                var trkPoint = new GpxTrkTrksegTrkpt();
                var time = recordMessage.FieldValue<uint>(RecordMesg.FieldDefNum.Timestamp);
                trkPoint.time = new Dynastream.Fit.DateTime(time).GetDateTime();

                trkPoint.lat = recordMessage.FieldValue<decimal>(RecordMesg.FieldDefNum.PositionLat) / 11930465;
                trkPoint.lon = recordMessage.FieldValue<decimal>(RecordMesg.FieldDefNum.PositionLong) / 11930465;
                trkPoint.ele = recordMessage.FieldValue<decimal>(RecordMesg.FieldDefNum.Altitude);

                trkPoint.extensions = new GpxTrkTrkptExtensions()
                {
                    TrackPointExtension = new TrackPointExtension()
                    {
                        atemp = recordMessage.FieldValue<decimal>(RecordMesg.FieldDefNum.Temperature),
                        cad = recordMessage.FieldValue<short>(RecordMesg.FieldDefNum.Cadence),
                        hr = recordMessage.FieldValue<short>(RecordMesg.FieldDefNum.HeartRate),
                        speed = recordMessage.FieldValue<short>(RecordMesg.FieldDefNum.Speed)
                    }
                };

                points.Add(trkPoint);
            }
        };

        mesgBroadcaster.MesgEvent += (sender, e) => { };

        fitDecoder.MesgEvent += mesgBroadcaster.OnMesg;

        using (var fitStream = new MemoryStream(data))
        {
            fitDecoder.Read(fitStream);
        }

        return new Gpx
        {
            version = (decimal)1.1,
            creator = "Mover",

            trk = new List<GpxTrk>()
            {
                new GpxTrk()
                {
                    trkseg = new List<GpxTrkTrkseg>()
                    {
                        new GpxTrkTrkseg(){
                            trkpt = points.ToArray(),
                        }
                    }.ToArray()
                }
            }.ToArray()
        };
    }
}
