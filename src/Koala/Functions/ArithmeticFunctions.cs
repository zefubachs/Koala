using System.Collections;

namespace Koala.Functions;
public class ArithmeticFunctions
{
    [Function]
    public int Length(object input)
    {
        if (input is string s)
            return s.Length;

        if (input is IEnumerable enumerable)
            return enumerable.Cast<object>().Count();

        throw new NotSupportedException($"Cannot get length of '{input}'.");
    }
}
