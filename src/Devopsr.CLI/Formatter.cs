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
            WriteErrorResult(result);
        }
    }

    public static void PrintResult(Result result, string successMessage)
    {
        if(result.IsSuccess)
        {
            Console.WriteLine("✅ " + successMessage);
        }
        else
        {
            WriteErrorResult(result);
        }
    }

    public static void WriteErrorResult<T>(Result<T> result)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine($"❌ {string.Join(", ", result.Errors.Select(e =>
        {
            return e.Message;
        }))}");
        Console.ForegroundColor = previousColor;
    }

    public static void WriteErrorResult(Result result)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine($"❌ {string.Join(", ", result.Errors.Select(e =>
        {
            return e.Message;
        }))}");
        Console.ForegroundColor = previousColor;
    }
}
