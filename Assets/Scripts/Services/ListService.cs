using System;
using System.Collections.Generic;

namespace Services
{
    internal static class ListService
    {
        public static T Min<T>(List<T> list, Converter<T, float> key)
        {
            if (list.Count <= 0)
                throw new ArgumentException("You good ?");
            T min = list[0];
            foreach (T t in list)
            {
                if (key(t) < key(min))
                    min = t;
            }
            return min;
        }

        public static List<U> ForEach<T, U>(List<T> list, Func<T, U> converter)
        {
            List<U> result = new List<U>();
            foreach (T item in list)
                result.Add(converter(item));
            return result;
        }

        /// <summary>
        /// creates a list of string TODO
        /// </summary>
        /// <param name="lowerBound">inclusive</param>
        /// <param name="upperBound">inclusive</param>
        /// <param name="step"></param>
        /// <returns>a list of string</returns>
        public static List<string> StringRange(int lowerBound, int upperBound, int step = 1)
        {
            int length = (int)Math.Floor(Math.Log10(upperBound)) + 1;
            List<string> list = new List<string>();
            for (int i = lowerBound; i <= upperBound; i += step)
            {
                list.Add(StringService.NormalizeNumber(i.ToString(), length));
            }
            return list;
        }
    }
}
