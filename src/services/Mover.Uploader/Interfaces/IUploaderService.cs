﻿using Mover.Shared.Models;

namespace Mover.Uploader.Interfaces;

public interface IUploaderService
{
    Task<Event?> UploadAsync(Guid rawId, string fileName, string userId, byte[] content, CancellationToken cancellationToken);
}
