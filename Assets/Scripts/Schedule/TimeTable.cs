using System;
using System.Collections.Generic;

using Pathfinding;
using Services;

namespace Schedule
{
    public class TimeTable
    {
        public List<Period> Periods;

        /// <summary>
        /// This class is supposed to be instantiated from Init only, not from the constructor
        /// </summary>
        public TimeTable()
        {
            Periods = new List<Period>();

            // TODO remove this once we can import TimeTables
            // Adding periods for tests
            //Periods.Add(new Period());
        }

        /// <summary>
        /// This class is supposed to be instantiated from Init only, not from the constructor
        /// </summary>
        public TimeTable(List<Period> periods) 
        {
            Periods = periods;
        }

        public string ToJson()
        {
            // TODO
            //return string.Join("\n", ListService.ForEach(Periods, period => period.ToJson()));
            throw new NotImplementedException();
        }

        public static TimeTable FromJson(string json)
        {
            // TODO
            throw new NotImplementedException();
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