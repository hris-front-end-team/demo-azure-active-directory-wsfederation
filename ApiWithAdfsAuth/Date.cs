using System;

namespace ApiWithAdfsAuth
{
    public static class Date
    {
        public static string FORMAT = "ddd, dd MMM yyyy HH:mm:ss";
        public static string SUFFIX = "(UTC ~ GMT)";

        public static string Format(DateTime dateTime)
        {
            return dateTime.ToString(FORMAT) + " " + SUFFIX;
        }

        public static string Format(DateTimeOffset? dateTime)
        {
            return dateTime == null ? "<NULL>" : dateTime.Value.ToString(FORMAT) + " " + SUFFIX;
        }
    }
}
