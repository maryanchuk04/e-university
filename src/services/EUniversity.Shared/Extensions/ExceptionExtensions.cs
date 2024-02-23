using System.Runtime.CompilerServices;

namespace EUniversity.Shared.Extensions;

public static class ExceptionExtensions
{
    public static T ThrowIfNull<T>(this T argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(argument);

        return argument;
    }
}
