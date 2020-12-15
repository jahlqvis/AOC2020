using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day13
{
    static class busschedules
    {
        static string[] testnote = new string[]
        {
            "939",
            "7,13,x,x,59,x,31,19"
        };

        static string[] note = new string[]
        {
            "1000677",
            "29,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,661,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,23,x,x,x,x,x,x,x,521,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,19"
        };

        static int earliest;
        static Dictionary<int, int> buslist = new Dictionary<int, int>();

        public static void init(bool test = false)
        {
            if (test)
                note = testnote;

            earliest = int.Parse(note[0]);
            string[] buses = note[1].Split(',');
            int n;
            foreach (var bus in buses)
                if (bus != "x" && int.TryParse(bus, out n))
                    buslist.Add(n, 0);
        }

        static void calculatewaitingtimetosnearest()
        {
            List<int> keys = new List<int>(buslist.Keys);
            foreach (int key in keys)
            {
                int i= 0;
                while (key * i < earliest)
                    i++;
                

                buslist[key] = key * i - earliest;
            }



            foreach (var bus in buslist)
            {
                
            }
        }

        public static int getnearestbustime()
        {
            calculatewaitingtimetosnearest();

            int waitingtime = int.MaxValue;
            int busid = 0;

            foreach (var bus in buslist)
            {
                if (waitingtime > bus.Value)
                {
                    waitingtime = bus.Value;
                    busid = bus.Key;
                }
            }

            return busid * waitingtime;
        }

    }




    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Code of advent 2020 - Day 13");
            sw.Start();
            busschedules.init(false);
            int res = busschedules.getnearestbustime();
            sw.Stop();
            Console.WriteLine($"The ID of the earliest bus you can take multiplied by the number of minutes you'll need to wait is: {res}");
            Console.WriteLine("Time elapsed for day 13 part1 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
