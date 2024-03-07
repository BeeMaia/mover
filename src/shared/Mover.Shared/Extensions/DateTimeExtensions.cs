namespace Mover.Shared.Extensions;

public static class DateTimeExtensions
{
    private static DateTime EpochStartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static long ToEpoch(this DateTime dateTime)
    {
        long epochTime = (long)(dateTime - EpochStartTime).TotalSeconds;

        return epochTime;
    }

    public static DateTime ToDateTime(this long epochTime)
    {
        return EpochStartTime.AddSeconds(epochTime);
    }
}
