using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day10
{
    static class adapterChecker
    {
        public static int[] input;

        public static int[] testadapters1 = new int[]
        {
            16,
            10,
            15,
            5,
            1,
            11,
            7,
            19,
            6,
            12,
            4
        };

        public static int[] testadapters2 = new int[]
        {
            28,
            33,
            18,
            42,
            31,
            14,
            46,
            20,
            48,
            47,
            24,
            23,
            49,
            45,
            19,
            38,
            39,
            11,
            1,
            32,
            25,
            35,
            8,
            17,
            7,
            9,
            4,
            2,
            34,
            10,
            3
        };

        static int outageJolts = 0;

        public static long search(int[] list)
        {
            List<int> adapterList = new List<int>(list);
            

            adapterList.Sort();
            LinkedList<int> adapterList2 = new LinkedList<int>(adapterList);

            int diff1 = 0;
            int diff3 = 0;
            var n = adapterList2.First;
            int i = adapterList2.Count;

            long res = 0;
            do
            {
                var prevprev = n?.Previous?.Previous?.Value;
                var prev = n?.Previous?.Value;
                var next = n?.Next?.Value;

                if (prev + 1 == n.Value && next == n.Value + 1)
                    diff1++;

                if (prev + 1 == n.Value && next == n.Value + 1 && prevprev +2 == n.Value)
                    diff1--;

                i--;
                n = n?.Next;
            } while(i>0);
             
            return (long)Math.Pow(2, diff1);
        }

        public static void getinputfromfile()
        {
            string[] temp = System.IO.File.ReadAllLines(@"C:\Users\JAH016\Source\Repos\AOC2020\Day10\input.txt");
            input = new int[temp.Length];
            int i = 0;
            foreach (var t in temp)
            {
                input[i] = int.Parse(t);
                i++;
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Code of advent 2020 - Day 10");
            //adapterChecker.getinputfromfile();
            var res = adapterChecker.search(adapterChecker.testadapters2);
            Console.WriteLine(res);
            Console.WriteLine("Time elapsed for day10 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
