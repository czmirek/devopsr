namespace Devopsr.Lib.Interfaces.Project;

public interface IProjectService
{
    /// <summary>
    /// Creates a new project file at the specified path.
    /// </summary>
    /// <param name="filePath">The path to the .devopsr file to create.</param>
    /// <returns>A result message indicating success or error.</returns>
    string CreateNewProject(string filePath);
}
