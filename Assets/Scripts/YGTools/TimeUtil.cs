using System;

public class TimeUtil
{
    public static DateTime MillsToDate(long mills)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        DateTime dt = startTime.AddMilliseconds(mills);
        return dt;
    }

    public static DateTime SecondsToDate(long senonds)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        DateTime dt = startTime.AddSeconds(senonds);
        return dt;
    }

    public static long DateToMills(DateTime date)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (long)(date - startTime).TotalMilliseconds;
    }

    public static long DateToSeconds(DateTime date)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (long)(date - startTime).TotalSeconds;
    }

    public static long Now(){
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }
}