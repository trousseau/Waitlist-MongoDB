using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace WaitlListMongoDB.Models
{
    public static class WailistHelpers
    {
        public static TimeSpan GetAvgWait(List<Person> people)
        {
            List<TimeSpan> waitTimes = new List<TimeSpan>();

            foreach (var p in people)
            {
                waitTimes.Add(p.GetTimeWaitedSpan());
            }

            double doubleAverageTicks = waitTimes.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

            return new TimeSpan(longAverageTicks);
        }

        public static TimeSpan GetAvgWait(IEnumerable<Person> people)
        {
            List<TimeSpan> waitTimes = new List<TimeSpan>();

            foreach (var p in people)
            {
                waitTimes.Add(p.GetTimeWaitedSpan());
            }

            double doubleAverageTicks = waitTimes.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

            return new TimeSpan(longAverageTicks);
        }

        public static string DisplayAvgWait(IEnumerable<Person> people)
        {
            TimeSpan avgWait = new TimeSpan();
            avgWait = GetAvgWait(people);

            return string.Format($"Hrs: {avgWait.Hours} Mins: {avgWait.Minutes}");
        }

        //todo: implement json to csv parser
        public static string ToCSV(this DataTable table, string delimitor)
        {
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : delimitor);
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : delimitor);
                }
            }
            return result.ToString().TrimEnd(new char[] { '\r', '\n' });
            //return result.ToString();
        }


    }
}