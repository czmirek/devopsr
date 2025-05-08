using FluentResults;

public static class Formatter
{
    public static void PrintResult<T>(Result<T> result, string successMessage)
    {
        if(result.IsSuccess)
        {
            Console.WriteLine("✅ " + successMessage);
        }
        else
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"❌ Error: {string.Join(", ", result.Errors.Select(e =>
            {
                return e.Message;
            }))}");
            Console.ForegroundColor = previousColor;
        }
    }
}
