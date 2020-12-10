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

            int diff0 = 0;
            int diff1 = 0;
            int diff2 = 0;
            int diff3 = 0;
            int previous = 0;
            int combi = 0;

            int[] l = new int[3];
            l[0] = 0;
            l[1] = 0;
            l[2] = 0;
            foreach (var a in adapterList)
            {

                switch(a-previous)
                {
                    case 0:
                        diff0++;
                        break;
                    case 1:
                        diff1++;
                        
                        break;
                    case 2:
                        diff2++;
                        break;
                    case 3:
                        diff3++;
                        break;
                    default:
                        throw new ArgumentException("Difference between two adapter larger than 3");
                }
                previous = a;
                combi += shift(ref l, a);

            }
            diff3++;

            return combi;

            //return permutations(combi);

            //return diff1 * diff3;


        }

        private static int shift(ref int[] list, int new_number)
        {
            int result = 0;

            if (new_number == list[0] + 1 && new_number == list[1] + 2)
                result = 1;

            int length = list.Length;

            for (int i = length-1; i > 0; i--)
                list[i] = list[i - 1];
            
            list[0] = new_number;

            return result;
        }

        public static long permutations(int input)
        {
            long res=1;
            for (int i = input; i > 0; i--)
                res *= i;
            return res;
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
            var res = adapterChecker.search(adapterChecker.testadapters1);
            Console.WriteLine(res);
            Console.WriteLine("Time elapsed for day10 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
