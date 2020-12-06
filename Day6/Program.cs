using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace Day6
{
    class Program
    {
        static class scanner
        {
            static string[] testinput = new string[]
            {
                "abc",
                "",
                "a",
                "b",
                "c",
                "",
                "ab",
                "ac",
                "",
                "a",
                "a",
                "a",
                "a",
                "",
                "b"
            };

            static string[] questionnaire;

            public static void getinputfromfile()
            {
                questionnaire = System.IO.File.ReadAllLines(@"C:\Users\JAH016\Source\Repos\AOC2020\Day6\input.txt");

            }
            public static int countallquestions()
            {
                List<string> groupanswers = new List<string>();

                int questions = 0;
                foreach(var str in questionnaire)
                {
                    if(str != "")
                    {
                        groupanswers.Add(str);
                    }
                    else
                    {
                        questions += countgroupeveryoneyes(ref groupanswers);
                        groupanswers.Clear();
                    }
                }
                questions += countgroupeveryoneyes(ref groupanswers);

                return questions;
            }

            public static int countgroupeveryoneyes(ref List<string> groupanswers)
            {
                Dictionary<char, int> counter = new Dictionary<char, int>();

                int questions = 0;
                int person = 0;
                int numpersons = groupanswers.Count();
                int value = 0;
                foreach (var q in groupanswers)
                {
                    
                    foreach (var c in q)
                    {

                        if (!counter.TryGetValue(c, out value))
                        {
                            value = 1;
                            counter.Add(c, value);
                        }
                        else
                        {
                            value += 1;
                            counter[c] = value;
                        }
                    }
                    person++;
                }

                foreach(var q in counter)
                {
                    if (q.Value == numpersons)
                        questions++;
                }

                return questions;
                
            }

            static int countgroupanswers(ref List<string> groupanswers)
            {
                List<char> questions = new List<char>();

                foreach(string q in groupanswers)
                {
                    foreach(char c in q)
                    {
                        if (!questions.Contains(c))
                            questions.Add(c);
                    }
                }

                return questions.Count;
            }

        }

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Code of advent 2020 - Day 6");
            sw.Start();
            scanner.getinputfromfile();

            var res = scanner.countallquestions();
            sw.Stop();
            Console.WriteLine(res);
            Console.WriteLine("Time elapsed for day6 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
