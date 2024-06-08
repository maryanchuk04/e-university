using System.Runtime.CompilerServices;

namespace EUniversity.Shared.Extensions;

public static class ExceptionExtensions
{
    public static T ThrowIfNull<T>(this T argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);

        return argument;
    }

    public static T ThrowIfNullOrDefault<T>(this T argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument == null)
        {
            throw new ArgumentNullException(paramName);
        }

        if (EqualityComparer<T>.Default.Equals(argument, default))
        {
            throw new ArgumentException($"Argument {paramName} cannot be the default value.", paramName);
        }

        return argument;
    }
}
