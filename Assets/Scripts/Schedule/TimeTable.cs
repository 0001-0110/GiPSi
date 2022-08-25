using System;
using System.Collections.Generic;

using Pathfinding;
using Services;

namespace Schedule
{
    public class TimeTable
    {
        public List<Period> Periods;

        // TODO move this to a proper location (or remove it)
        private static T GetRandomEnumValue<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("This method only takes Enum types");
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(new Random().Next(values.Length));
        }

        /// <summary>
        /// This class is supposed to be instantiated from Init only, not from the constructor
        /// </summary>
        public TimeTable()
        {
            Periods = new List<Period>();

            // TODO remove this once we can import TimeTables
            // Adding periods for tests
            Periods.Add(new Period(GetRandomEnumValue<Subject>(), DateTime.Now, DateTime.Now.AddMinutes(6), Destination.Room207));
            Periods.Add(new Period(GetRandomEnumValue<Subject>(), DateTime.Now.AddMinutes(6), DateTime.Now.AddMinutes(7), GetRandomEnumValue<Destination>()));
            Periods.Add(new Period(GetRandomEnumValue<Subject>(), DateTime.Now.AddMinutes(7), DateTime.Now.AddMinutes(60), GetRandomEnumValue<Destination>()));
        }

        /// <summary>
        /// This class is supposed to be instantiated from Init only, not from the constructor
        /// </summary>
        public TimeTable(List<Period> periods) 
        {
            Periods = periods;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public Destination GetRoom(DateTime time)
        {
            foreach (Period period in Periods)
            {
                if (time >= period.StartTime && time <= period.EndTime)
                    return period.Room;
            }
            return Destination.None;
        }

        public Destination GetActiveRoom()
        {
            return GetRoom(DateTime.Now.AddMinutes(5));
        }
    }
}