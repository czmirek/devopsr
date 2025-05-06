using System;

namespace Devopsr.Lib.Services;

public sealed class CreateNewProjectRequest
{
    public required string FilePath { get; init; }
}
