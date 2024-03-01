using Dapr;
using Microsoft.AspNetCore.Http;
using Mover.Modules.Uploader.Shared.Commands;
using Mover.Shared;
using Mover.Shared.Dispatchers;
using Mover.Shared.Interfaces;

namespace Mover.Modules.Uploader;

public class UploaderDispatcher
{
    [Topic(Constants.Dapr.MOVER_PUBSUB, "uploadfile")]
    public static Task<IResult> HandleUploadFileAsync(UploadFile command, ICommandHandler<UploadFile> commandHandler, CancellationToken cancellationToken)
    {
        return CommandDispatcher.DispatchAsync(command, commandHandler, cancellationToken);
    }
}
