using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day13
{
    public static class busschedules
    {

        public static string[] testinput = new string[]
{
            "3417",
            "17,x,13,19"
};

        public static string[] input = new string[]
        {
            "1000677",
            "29,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,661,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,23,x,x,x,x,x,x,x,521,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,19"
        };

        static ulong earliest;
        static Dictionary<ulong, ulong> busdict = new Dictionary<ulong, ulong>();
        static List<ulong> busses;
        
        public static void init(string[] note)
        {
            earliest = ulong.Parse(note[0]);

            string[] buses = note[1].Split(',');
            ulong i, n;
            int length = buses.Length;

            busdict.Clear();
            i = 0;
            foreach (var bus in buses)
            {
                if (bus != "x" && ulong.TryParse(bus, out n))
                {
                    busdict.Add(n, i);
                }
                i++;
            }

            busses = busdict.Keys.ToList();
    
            
        }

        public static ulong getstep(List<ulong> buslist)
        {
            var r = 1ul;
            foreach (var b in buslist)
            {
                r = r * b;
            }
            return r;
        }

        public static ulong startsearch()
        {
            // main loop
            ulong time=0;

            var stepbusses = new List<ulong>();

            busses.Sort();
            busses.Reverse();

            var is_valid = false;

            while(!is_valid)
            {
                is_valid = true;
                var step = getstep(stepbusses);
                time = time + step;
                
                foreach (var bus in busses)
                {
                    var bustime = time + busdict[bus];
                    var mod = bustime % bus;
                    if (mod == 0)
                    {
                        if(!stepbusses.Contains(bus))
                        {
                            stepbusses.Add(bus);
                            Console.WriteLine($"Added bus {bus} offset {busdict[bus]}");

                        }
                    }
                    else
                    {
                        is_valid = false;
                        break;
                    }
                }
                
                if (is_valid)
                    break;
            }

            return time;
        }

        static void calculatewaitingtimetosnearest()
        {
            List<ulong> keys = new List<ulong>(busdict.Keys);
            foreach (ulong key in keys)
            {
                ulong i= 0;
                while (key * i < earliest)
                    i++;
                

                busdict[key] = key * i - earliest;
            }
        }

        public static ulong getnearestbustime()
        {
            calculatewaitingtimetosnearest();

            ulong waitingtime = int.MaxValue;
            ulong busid = 0;

            foreach (var bus in busdict)
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
            busschedules.init(busschedules.input);
            var res = busschedules.startsearch();
            sw.Stop();
            Console.WriteLine($"Earliest time is {res}");
            Console.WriteLine("Time elapsed for day 13 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
