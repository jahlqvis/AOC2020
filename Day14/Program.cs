using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day14
{
    static class emulator
    {
        public static string[] input;

        static public void GetInputFromFile()
        {
            input = System.IO.File.ReadAllLines(@"input.txt");

        }

        public static string[] testinput = new string[]
        {
            "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
            "mem[8] = 11",
            "mem[7] = 101",
            "mem[8] = 0"
        };

        static Dictionary<int, long> _memory = new Dictionary<int, long>();


        static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static long ParseProgram(string[] program)
        {
            bool reachedEnd = false;
            int i = 0;
            int lengthOfProgram = program.Length;
            string reversedMaskString = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"; 
            
            while(!reachedEnd)
            {
                reachedEnd = true;

                string[] substrs = program[i].Split(" = ");

                // mask
                if (substrs[0].Equals("mask"))
                {
                    reversedMaskString = Reverse(substrs[1]);
                }
                // memory location and value
                else if (substrs[0].Contains("mem"))
                {
                    string memoryAddressString = Regex.Match(substrs[0], @"\d+").Value;
                    int memoryAddress = int.Parse(memoryAddressString);

                    string valueString = Regex.Match(substrs[1], @"\d+").Value;
                    string valueBinary = Convert.ToString(Convert.ToInt64(valueString, 10), 2);

                    string reversedValueString = Reverse(valueBinary);
                    char[] reversedResultString = new char[36];

                    //string binaryString = Convert.ToInt64(valueString, 2).ToString();

                    for(int j = 0; j < reversedMaskString.Length; j++)
                    {
                        int valueStringLength = reversedValueString.Length;
                        switch(reversedMaskString[j])
                        {
                            case 'X':
                                if (j < valueStringLength)
                                    reversedResultString[j] = reversedValueString[j];
                                else
                                    reversedResultString[j] = '0';
                                break;
                            case '1':
                                reversedResultString[j] = '1';
                                break;
                            case '0':
                                reversedResultString[j] = '0';
                                break;
                            default:
                                throw new ArgumentException("Something wong with mask string");
                        }
                    }

                    string resultString = Reverse(new string(reversedResultString)); // re-reverse
                    long result = Convert.ToInt64(resultString, 2);

                    if (_memory.ContainsKey(memoryAddress))
                        _memory[memoryAddress] = result;
                    else
                        _memory.Add(memoryAddress, result);
                }

                i++;
                if (i < lengthOfProgram)
                    reachedEnd = false;

                if (reachedEnd)
                    break;
            }

            long sum = 0;
            foreach (var reg in _memory)
                sum = sum + reg.Value;

            return sum;
       }

    }



    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Code of advent 2020 - Day 14");
            sw.Start();
            emulator.GetInputFromFile();
            var res = emulator.ParseProgram(emulator.input);
            sw.Stop();
            Console.WriteLine($"Sum is {res}");
            Console.WriteLine("Time elapsed for day 14 part2 .NET Core 3 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
