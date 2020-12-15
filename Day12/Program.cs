using System;
using System.Diagnostics;


namespace Day12
{
    public class Position
    {
        int _east = 0;
        int _north = 0;
        double _bearing = 0;
        double _absolutedistance = 0;

        public void rotateLeft(double degrees)
        {
            if (_bearing - degrees < 0)
                _bearing = 360 + (_bearing - degrees);
            else
                _bearing -= degrees;

            Debug.Assert(!double.IsNaN(_bearing));
            updatePositionByDegree(_bearing);
        }

        public void rotateRight(double degrees)
        {
            if (_bearing + degrees >= 360)
                _bearing = (_bearing + degrees) - 360;
            else
                _bearing += degrees;


            updatePositionByDegree(_bearing);
        }

        private void updateAbsDistance()
        {
            _absolutedistance = Math.Sqrt(Math.Pow(_east, 2) + Math.Pow(_north, 2));
        }

        private void updatePositionByDegree(double degree)
        {
            updateAbsDistance();

            double e = (_absolutedistance * Math.Cos(Math.PI / 180d * degree));
            double n = (_absolutedistance * Math.Sin(Math.PI / 180d * degree));

            Debug.Assert(!double.IsNaN(degree));
            // The four quadrants (opposite to normal order)
            // I. 0 - 270 degrees
            if (degree < 360 && degree >= 270)
            {
                _east = (int)Math.Round(e, 0);
                _north = (int)-Math.Round(n, 0);
            }
            // II. 270 - 180
            else if (degree < 270 && degree >= 180)
            {
                _east = (int)Math.Round(e, 0);
                _north = (int)-Math.Round(n, 0);
            }
            // III. 180 - 90
            else if (degree < 180 && degree >= 90)
            {
                _east = (int)Math.Round(e, 0);
                _north = (int)-Math.Round(n, 0);
            }
            // IV. 90 - 0
            else if (degree < 90 && degree >= 0)
            {
                _east = (int)Math.Round(e, 0);
                _north = (int)-Math.Round(n, 0);
            }
            else
            {
                throw new ArgumentException($"Something went wong. Parameter degree is {degree}");
            }

        }

        private void updateBearing()
        {
            if (_east == 0 && _north > 0)
                _bearing = 270;
            else if (_east == 0 && _north < 0)
                _bearing = 90;
            else if (_east > 0 && _north == 0)
                _bearing = 0;
            else if (_east < 0 && _north == 0)
                _bearing = 180;
            else if (_east == 0 && _north == 0)
                _bearing = 0;
            // The four quadrants (opposite to normal order)
            // I. 0 - 270 degrees
            else if (_east >= 0 && _north >= 0)
            {
                _bearing = 360 - Math.Atan((double)_north / (double)_east) * 180 / Math.PI; ;
            }
            // II. 270 - 180
            else if (_east < 0 && _north >= 0)
            {
                _bearing = 180 + Math.Atan((double)_north / (double)-_east) * 180 / Math.PI;
            }
            // III. 180 - 90
            else if (_east < 0 && _north < 0)
            {
                _bearing = 180 - Math.Atan((double)-_north / (double)-_east) * 180 / Math.PI;
            }
            // IV. 90 - 0
            else if (_east >= 0 && _north < 0)
            {
                _bearing = Math.Atan((double)_north / (double)-_east) * 180 / Math.PI;
            }
            else
                throw new ArgumentException("Something went wong");

            Debug.Assert(!double.IsNaN(_bearing));

            updateAbsDistance();

        }

        public void moveEast(int value)
        {
            _east += value;
            updateBearing();
        }
        public void moveWest(int value)
        {
            _east -= value;
            updateBearing();
        }
        public void moveNorth(int value)
        {
            _north += value;
            updateBearing();
        }
        public void moveSouth(int value)
        {
            _north -= value;
            updateBearing();
        }

        public int getManhattanPosition()
        {
            return Math.Abs(_east) + Math.Abs(_north);
        }

        public int getEast()
        {
            return _east;
        }

        public int getNorth()
        {
            return _north;
        }
    }


    public static class Boat
    {
        public static Position position = new Position();
        public static Position waypointposition = new Position();

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
            // initial way point pos
            waypointposition.moveEast(10);
            waypointposition.moveNorth(1);

            foreach (var action in instructions)
            {
                int length = action.Length;
                char operand = action[0];
                string strvalue = action.Substring(1, length - 1);
                int value = int.Parse(strvalue);

                switch (operand)
                {
                    case 'F':
                        int north = waypointposition.getNorth();
                        int east = waypointposition.getEast();

                        if (north >= 0)
                            position.moveNorth(value * north);
                        else
                            position.moveSouth(-value * north);

                        if (east >= 0)
                            position.moveEast(value * east);
                        else
                            position.moveWest(-value * east);
                        break;
                    case 'E':
                        waypointposition.moveEast(value);
                        break;
                    case 'W':
                        waypointposition.moveWest(value);
                        break;
                    case 'N':
                        waypointposition.moveNorth(value);
                        break;
                    case 'S':
                        waypointposition.moveSouth(value);
                        break;
                    case 'L':
                        waypointposition.rotateLeft(value);
                        break;
                    case 'R':
                        waypointposition.rotateRight(value);
                        break;
                    default:
                        throw new ArgumentException($"Could not understand instruction {operand} {value}");

                }
                //Console.WriteLine($"Boat is now at east {Boat.position.getEast()}, north {Boat.position.getNorth()}");
                //Console.WriteLine($"Waypoint is at east {Boat.waypointposition.getEast()}, north {Boat.waypointposition.getNorth()}");
                //Console.WriteLine();

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
            //Boat.loadtestinstructions();
            Boat.followInstructions();
            int res = Boat.position.getManhattanPosition();
            sw.Stop();
            Console.WriteLine($"Manhattan position of boat is: {res}");
            Console.WriteLine("Time elapsed for day 12 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}

