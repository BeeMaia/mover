using Dapr;
using Mover.Shared;
using Mover.Shared.Dispatchers;
using Mover.Shared.Interfaces;
using Mover.Uploader.Shared.Commands;

namespace Mover.Uploader;

public class UploaderDispatcher
{
    [Topic(Constants.Dapr.MOVER_PUBSUB, UploadFile.Topic)]
    public static Task<IResult> HandleUploadFileAsync(UploadFile command, ICommandHandler<UploadFile> commandHandler, CancellationToken cancellationToken)
    {
        return CommandDispatcher.DispatchAsync(command, commandHandler, cancellationToken);
    }
}
