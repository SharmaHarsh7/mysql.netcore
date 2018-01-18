using System;

namespace DS.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsDefaultDate(this DateTime dt)
        {
            if (dt.GetHashCode() == 0)
                return true;

            return false;
        }

        public static DateTime GetLastDayOfMonth(this DateTime dt)
        {
            DateTime first = new DateTime(dt.Year, dt.Month, 1);

            DateTime last = first.AddMonths(1).AddSeconds(-1);

            return last;
        }

        public static DateTime GetFirstDayOfMonth(this DateTime dt)
        {
            DateTime first = new DateTime(dt.Year, dt.Month, 1);

            return first;
        }
    }
}
