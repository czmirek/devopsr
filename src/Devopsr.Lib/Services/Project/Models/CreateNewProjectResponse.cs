using System;

namespace Devopsr.Lib.Services;

public sealed class CreateNewProjectResponse
{
    public required bool Success { get; init; }
    public required string Message { get; init; }
}
