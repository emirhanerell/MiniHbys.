namespace MiniHbys.Utilities;

public static class Extensions
{
    public static int ToInt32(this object obj)
    {
        var result = 0;

        if (obj != null)
        {
            if (int.TryParse(obj.ToString(), out var value))
                result = value;
        }

        return result;
    }
    
    public static DateTime ToDateTime(this object obj)
    {
        var result = DateTime.MinValue;

        if (obj != null)
        {
            if (DateTime.TryParse(obj.ToString(), out var value))
            {
                return value;
            }
        }

        return result;
    }

    public static bool ToBoolean(this object obj)
    {
        var result = false;
        if(obj != null)
            bool.TryParse(obj.ToString(), out result);
        return result;
    }
}