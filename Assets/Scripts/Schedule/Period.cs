using System;
using System.Text.RegularExpressions;

using Pathfinding;

namespace Schedule
{
    public class Period
    {
        private static Regex regexFromJson = new Regex("{}", RegexOptions.Multiline);

        public Subject Subject { get; private set; }
        // the time when this period is starting
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public Destination Room { get; private set; }
        
        public Period(Subject subject, DateTime startTime, DateTime endTime, Destination room)
        {
            Subject = subject;
            StartTime = startTime;
            EndTime = endTime;
            Room = room;
        }

        public string ToJson()
        {
            //return JsonSerializer.Serialize();
            //return $"{{\n\"Subject\": {Subject},\n\"StartTime\": {StartTime},\n\"EndTime\": {EndTime},\n\"Room\": {Room},\n}}";
            throw new NotImplementedException();
        }

        public static Period FromJson(string json)
        {
            /*Match match = regexFromJson.Match(json);
            Subject subject = ;
            DateTime startTime = ;
            DateTime endTime = ;
            Destination destination = ;
            return new Period(subject, startTime, endTime, destination);*/
            throw new NotImplementedException();
        }
    }
}
