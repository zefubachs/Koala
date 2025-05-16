namespace Koala.Extensions;
public static class ObjectExtension
{
    public static bool AsBoolean(this object? obj)
    {
        if (obj is bool b)
            return b;

        if (obj is string s)
            return string.IsNullOrWhiteSpace(s);

        if (obj is double d)
            return d > 0;

        return obj is not null;
    }

    public static bool IsNumber(this object? obj, out double number)
    {
        if (obj is null)
        {
            number = 0;
            return false;
        }

        var isNumber = obj is int or double or decimal or float or short or long;
        if (isNumber)
        {
            switch (obj)
            {
                case int i:
                    number = i;
                    break;
                case double d:
                    number = d;
                    break;
                case float f:
                    number = (double)f;
                    break;
                case short s:
                    number = s;
                    break;
                case long l:
                    number = l;
                    break;
                default:
                    number = 0;
                    return false;
            }
            return true;
        }
        else
        {
            number = 0;
            return false;
        }
    }
}
