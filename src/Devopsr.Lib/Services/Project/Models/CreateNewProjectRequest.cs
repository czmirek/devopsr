using System;

namespace Devopsr.Lib.Services.Project.Models
{
    public sealed class CreateNewProjectRequest
    {
        public required string FilePath { get; init; }
    }
}