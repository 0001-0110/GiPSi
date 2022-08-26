using System;

namespace Schedule
{
    public enum Subject
    {
        Mathematics,
        History,
        French,
        English,
        German,
        Spanish,
        Biology,
        Chemestry,
        Physics,
        PhysicalEducation,
    }

    public static class DateTimeExtension
    {
        // The idea was to create a schedule modulo 14 days to allow it to loop
        public static DateTime Modulo(this DateTime dateTime, int days)
        {
            throw new NotImplementedException();
        }
    }
}