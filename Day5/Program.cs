using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day5
{
    static class scanner
    {
        static string[] boardingpassestest = new string[]
        {
            "BFFFBBFRRR",//row 70, column 7, seat ID 567.
            "FFFBBBFRRR",//row 14, column 7, seat ID 119.
            "BBFFBBFRLL"//row 102, column 4, seat ID 820.
        };

        static string[] boardingpasses;
        static List<int> list = new List<int>();

        static public void getinputfromfile()
        {
            boardingpasses = System.IO.File.ReadAllLines(@"C:\Users\JAH016\Source\Repos\AOC2020\Day5\input.txt");

        }
        static void getpartition(char c, int n, int b, out int min, out int max)
        {
            

            if (c == 'F')
            {
                int k = 7 - n;
                int l = (int)Math.Pow(2, k);

                min = b+0;
                max = b+l/2-1;
            }
            else if (c == 'B')
            {
                int k = 7 - n;
                int l = (int)Math.Pow(2, k);

                min = b+l/2;
                max = b+l-1;
            }
            else if (c == 'L')
            {
                int k = 3 - n;
                int l = (int)Math.Pow(2, k);

                min = b + 0;
                max = b + l / 2 - 1;
            }
            else if (c == 'R')
            {
                int k = 3 - n;
                int l = (int)Math.Pow(2, k);

                min = b + l / 2;
                max = b + l - 1;
            }
            else
                throw new ArgumentException("char c is not valid");

        }

        public static int calculateRow(int passnum)
        {
            int min=128;
            int max=128;
            int b=0;
            for (int i=0;i<7;i++)
            {
                getpartition(boardingpasses[passnum][i], i, b, out min, out max);
                b = min;
            }

            return b;
        }

        public static int calculateCol(int passnum)
        {
            int min = 8;
            int max = 8;
            int b = 0;
            for (int i = 7; i < 10; i++)
            {
                getpartition(boardingpasses[passnum][i], i-7, b, out min, out max);
                b = min;
            }

            return b;
        }

        public static int highestSeatId()
        {
            int maxnumpass = boardingpasses.Length;
            int maxseatid = 0;
            int col;
            int row;
            int seatid;

            

            for(int i=0;i<maxnumpass;i++)
            {
                col = calculateCol(i);
                row = calculateRow(i);
                seatid = row * 8 + col;

                list.Add(seatid);

                if (seatid > maxseatid)
                    maxseatid = seatid;
            }
            return maxseatid;
        }

        public static int getmySeatId()
        {
            list.Sort();

            int k = list[0]; ;
            for (int i=0;i<list.Count;i++)
            {
                if(i != 0 && i != list.Count-1 && list[i] != k)
                {
                    return k;
                }
                k++;
            }
            return -1;
        }


    }
        class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Code of advent 2020 - Day 5");
            sw.Start();
            scanner.getinputfromfile();
            scanner.highestSeatId();
            var res = scanner.getmySeatId();
            sw.Stop();
            Console.WriteLine(res);
            Console.WriteLine("Time elapsed for day5 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
