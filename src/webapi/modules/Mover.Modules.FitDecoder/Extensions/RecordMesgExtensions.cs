using Dynastream.Fit;

namespace Mover.Modules.FitDecoder.Extensions;

internal static class RecordMesgExtensions
{
    public static T? FieldValue<T>(this RecordMesg recordMesg, byte fieldDefNum)
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
