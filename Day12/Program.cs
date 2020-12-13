using System;
using System.Diagnostics;

namespace Day12
{
    public static class Boat
    {
        public static class Direction
        {
            static int dir = 0;

            public static string Dir
            {
                get
                {
                    switch (dir)
                    {
                        case 0:
                        case 360:
                            return "East";
                        case 90:
                            return "South";
                        case 180:
                            return "West";
                        case 270:
                            return "North";
                        default:
                            throw new ArgumentException($"Unknown direction {dir}");
                    }
                }
            }

            public static void turnLeft(int degrees)
            {
                if (dir - degrees < 0)
                {
                    dir = 360 + (dir - degrees);
                }
                else
                    dir -= degrees;
            }

            public static void turnRight(int degrees)
            {
                if (dir + degrees > 360)
                {
                    dir = (dir + degrees) - 360;
                }
                else
                    dir += degrees;
            }
        }

        public static class Position
        {
            static int east = 0;
            static int north = 0;

            public static void moveEast(int value)
            {
                east += value;
            }
            public static void moveWest(int value)
            {
                east -= value;
            }
            public static void moveNorth(int value)
            {
                north += value;
            }
            public static void moveSouth(int value)
            {
                north -= value;
            }

            public static int getManhattanPosition()
            {
                return Math.Abs(east) + Math.Abs(north);
            }
        }

        static string[] testInstructions = new string[]
        {
            "F10",
            "N3",
            "F7",
            "R90",
            "F11"
        };

        static string[] instructions;

        static public void getinputfromfile()
        {
            instructions = System.IO.File.ReadAllLines(@"input.txt");
        }

        static public void loadtestinstructions()
        {
            instructions = testInstructions;
        }

        public static void followInstructions()
        {
            foreach (var action in instructions)
            {
                int length = action.Length;
                char operand = action[0];
                string strvalue = action.Substring(1, length - 1);
                int value = int.Parse(strvalue);

                if(operand == 'F')
                {
                    string direction = Direction.Dir;
                    operand = direction[0]; 
                }

                switch(operand)
                {
                    case 'E':
                        Position.moveEast(value);
                        break;
                    case 'W':
                        Position.moveWest(value);
                        break;
                    case 'N':
                        Position.moveNorth(value);
                        break;
                    case 'S':
                        Position.moveSouth(value);
                        break;
                    case 'R':
                        Direction.turnRight(value);
                        break;
                    case 'L':
                        Direction.turnLeft(value);
                        break;
                    
                    default:
                        throw new ArgumentException($"Could not understand instruction {operand} {value}");
                        
                }


            }


        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Code of advent 2020 - Day 12");
            sw.Start();
            Boat.getinputfromfile();
            Boat.followInstructions();
            int res = Boat.Position.getManhattanPosition();
            sw.Stop();
            Console.WriteLine($"Manhattan position of boat is: {res}");
            Console.WriteLine("Time elapsed for day 12 part1 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
