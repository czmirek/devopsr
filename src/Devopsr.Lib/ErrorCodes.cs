namespace Devopsr.Lib;

public static class ErrorCodes
{
    public const string InvalidProjectFileExtension = "InvalidProjectFileExtension";
    public const string ProjectFileAlreadyExists = "ProjectFileAlreadyExists";
    public const string ProjectFileDoesNotExist = "ProjectFileDoesNotExist";
    public const string NoProjectLoaded = "NoProjectLoaded";
    public const string ProjectFileDeserializationFailed = "ProjectFileDeserializationFailed";
    public const string NoProjectOpen = "NoProjectOpen";
    public const string ParentNodeNotFound = "ParentNodeNotFound";
    public const string NodeIdAlreadyExists = "NodeIdAlreadyExists";
    // Add more error codes as needed
}