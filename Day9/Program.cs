using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day9
{
    static class decipher
    {
        static ulong[] testinput = new ulong[]
        {
            35,
            20,
            15,
            25,
            47,
            40,
            62,
            55,
            65,
            95,
            102,
            117,
            150,
            182,
            127,
            219,
            299,
            277,
            309,
            576
        };

        static Queue<ulong> queue;

        static ulong[] input;

        public static ulong find_number()
        {
            int preamble = 25;
            

            int i = preamble;
            int found1;
            int found2;

            for(;i<input.Length;i++)
            {
                if (!search_for_pair(preamble, i, out found1, out found2))
                {
                    return input[i];
                }

            }

            return 0;
        
        }

        static bool search_contiguous_set(ulong sum)
        {
            queue = new Queue<ulong>();

            int i = 0;
            do
            {
                i++;
                if (i < input.Length)
                {
                    queue.Enqueue(input[i]);
                }

                int check = sum_checker(sum);
                if (check == 0)
                    return true;

                if (check > 0)
                    queue.Dequeue();

            } while(queue.Count>0);
            
            return false;
        }

        public static ulong find_magic_number(ulong sum)
        {
            if(search_contiguous_set(sum))
            {
                List<ulong> list = new List<ulong>();
                ulong sumcheck = 0;
                
                foreach(var number in queue)
                {
                    sumcheck += number;
                    if (sumcheck <= sum)
                    {
                        list.Add(number);
                    }
                        
                }

                list.Sort();

                return list[0] + list[list.Count - 1];
            }

            return 0;
        }
        static int sum_checker(ulong sum)
        {
            ulong temp_sum=0;
                        
            foreach(var number in queue)
            {
                temp_sum += number;
                if (temp_sum > sum)
                    return 1;

                if (temp_sum == sum)
                    return 0;

                
            }
            return -1;
        }

        static bool search_for_pair(int preamble, int current_index, out int found1, out int found2)
        {
            int i = current_index;
            for (int j = i - preamble; j < i; j++)
            {
                for (int k = i - preamble; k < i; k++)
                {
                    if (input[i] == (input[j] + input[k]))
                    {
                        found1 = j;
                        found2 = k;
                        return true;
                    }

                }
            }
            found1 = -1;
            found2 = -1;
            return false;

        }

        public static void getinputfromfile()
        {
            string[] temp = System.IO.File.ReadAllLines(@"C:\Users\JAH016\Source\Repos\AOC2020\Day9\input.txt");
            input = new ulong[temp.Length];
            int i = 0;
            foreach (var t in temp)
            {
                input[i] = ulong.Parse(t);
                i++;
            }

            //input = testinput;
        }

    }



    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Code of advent 2020 - Day 9");
            decipher.getinputfromfile();
            var res = decipher.find_number();
            res = decipher.find_magic_number(res);
            Console.WriteLine(res);
            Console.WriteLine("Time elapsed for day9 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
