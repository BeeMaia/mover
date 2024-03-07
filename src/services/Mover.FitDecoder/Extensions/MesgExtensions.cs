using Dynastream.Fit;

namespace Mover.FitDecoder.Extensions;

internal static class MesgExtensions
{
    public static T? FieldValue<T>(this Mesg recordMesg, byte fieldDefNum)
    {
        var value = GetFieldValue(recordMesg, fieldDefNum);
        if (value == null)
            return default;

        return (T)Convert.ChangeType(value, typeof(T));
    }

    private static object? GetFieldValue(Mesg mesg, byte fieldNumber)
    {
        var profileField = Profile.GetField(mesg.Num, fieldNumber);

        if (null == profileField)
        {
            return null;
        }

        var fields = mesg.GetOverrideField(fieldNumber);

        foreach (var field in fields)
        {
            if (field is Field)
            {
                return field.GetValue();
            }
            else
            {
                return field.GetValue();
            }
        }

        return null;
    }
}
