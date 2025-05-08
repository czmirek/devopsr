namespace Devopsr.Lib.Services;

public static class ErrorCodes
{
    public static readonly string InvalidProjectFileExtension = "InvalidProjectFileExtension";
    public static readonly string ProjectFileAlreadyExists = "ProjectFileAlreadyExists";
    public static readonly string ProjectFileDoesNotExist = "ProjectFileDoesNotExist";
    public static readonly string NoProjectLoaded = "NoProjectLoaded";
    public static readonly string ProjectFileDeserializationFailed = "ProjectFileDeserializationFailed";
    // Add more error codes as needed
}