using System;

namespace MasterService.Api.Utilities
{
    public static class Extensions
    {
        public static DateTime ToDateTime(this long timeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
