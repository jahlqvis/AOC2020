using System;
using System.Diagnostics;
using System.Text;

namespace Day11
{
    static public class scanner
    {
        static string[] _currentlayout;


        //static string[] _currentlayout = new string[]
        //{
        //    "L.LL.LL.LL",
        //    "LLLLLLL.LL",
        //    "L.L.L..L..",
        //    "LLLL.LL.LL",
        //    "L.LL.LL.LL",
        //    "L.LLLLL.LL",
        //    "..L.L.....",
        //    "LLLLLLLLLL",
        //    "L.LLLLLL.L",
        //    "L.LLLLL.LL"
        //};


        static string[] _cachelayout;

        const char floor = '.';
        const char empty = 'L';
        const char occupied = '#';
        const char outside = ' ';

        static int _currentOccupiedCount=0;
        static int _currentEmptyCount=0;
        static char[,] _adjacentmatrix;
        static int _adjacentOccupiedCount;
        static int _adjacentEmptyCount;
        static char _currentSeat;
        static int _iterations = 0;

        public static int CurrentOccupiedCount { get => _currentOccupiedCount; }
        public static int Iterations { get => _iterations; }

        public static void showSeats()
        {
            foreach (string s in _currentlayout)
                Console.WriteLine(s);
            Console.WriteLine("");
        }

        static void updateAdjacent(int row, int col)
        {
            _adjacentmatrix = new char[3, 3];
            _adjacentmatrix.Initialize();
            _adjacentEmptyCount = 0;
            _adjacentOccupiedCount = 0;

            int local_row = 0; int local_col = 0;
            for(int adjacent_row = row - 1; adjacent_row < row + 2; adjacent_row++)
            {
                local_col = 0;
                for (int adjacent_col = col - 1; adjacent_col < col + 2; adjacent_col++)
                {
                    if (adjacent_row >= 0 &&
                       adjacent_row < _currentlayout.Length &&
                       adjacent_col >= 0 &&
                       adjacent_col < _currentlayout[0].Length)
                    {
                        _adjacentmatrix[local_row, local_col] = _currentlayout[adjacent_row][adjacent_col];

                        if (local_col == 1 && local_row == 1)
                            _currentSeat = _currentlayout[adjacent_row][adjacent_col];
                        else if (_adjacentmatrix[local_row, local_col] == empty)
                            _adjacentEmptyCount++;
                        else if (_adjacentmatrix[local_row, local_col] == occupied)
                            _adjacentOccupiedCount++;
                        else
                            ;
                    }
                    else 
                        _adjacentmatrix[local_row, local_col] = outside;

                    local_col++;
                }
                local_row++;
            }
        }

        static int updateVisibilty(int row, int col)
        {
            int visibleOccupiedCount = 0;
            int r;
            int c;
            

            _currentSeat = _currentlayout[row][col];

            // up
            for (r=row-1;r>=0;r--)
            {
                if (_currentlayout[r][col] == occupied)
                {
                    visibleOccupiedCount++;
                    break;
                }
                else if(_currentlayout[r][col] == empty)
                    break;
            }
            // down
            for (r = row+1; r < _currentlayout.Length; r++)
            {
                if (_currentlayout[r][col] == occupied)
                {
                    visibleOccupiedCount++;
                    break;
                }
                else if (_currentlayout[r][col] == empty)
                    break;
            }
            // right
            for (c = col+1; c < _currentlayout[row].Length; c++)
            {
                if (_currentlayout[row][c] == occupied)
                {
                    visibleOccupiedCount++;
                    break;
                }
                else if(_currentlayout[row][c] == empty)
                    break;
            }
            // left
            for (c = col-1; c >= 0; c--)
            {
                if (_currentlayout[row][c] == occupied)
                {
                    visibleOccupiedCount++;
                    break;
                }
                else if(_currentlayout[row][c] == empty)
                    break;
            }
            //up-left
            r = row-1;
            c = col-1;
            for (; r >= 0 && c>=0; r--, c--)
            {
                if (_currentlayout[r][c] == occupied)
                {
                    visibleOccupiedCount++;
                    break;
                }
                else if (_currentlayout[r][c] == empty)
                    break;
            }
            //down-right
            r = row+1;
            c = col+1;
            for (; r < _cachelayout.Length && c < _cachelayout[r].Length; r++, c++)
            {
                if (_currentlayout[r][c] == occupied)
                {
                    visibleOccupiedCount++;
                    break;
                }
                else if (_currentlayout[r][c] == empty)
                    break;
            }
            //up-right
            r = row-1;
            c = col+1;
            for (; r >= 0 && c < _cachelayout[r].Length; r--, c++)
            {
                if (_currentlayout[r][c] == occupied)
                {
                    visibleOccupiedCount++;
                    break;
                }
                else if (_currentlayout[r][c] == empty)
                    break;
            }
            //down-left
            r = row+1;
            c = col-1;
            for (; r < _cachelayout.Length && c >= 0; r++, c--)
            {
                if (_currentlayout[r][c] == occupied)
                {
                    visibleOccupiedCount++;
                    break;
                }
                else if (_currentlayout[r][c] == empty)
                    break;
            }

            return visibleOccupiedCount;
        }

        static string replaceAtIndex(this string text, int index, char c)
        {
            var stringBuilder = new StringBuilder(text);
            stringBuilder[index] = c;
            return stringBuilder.ToString();
        }

        static bool applyRulesToCurrentSeat(int row, int col)
        {
            bool seatChanged = false;
            //updateAdjacent(row, col);
            int visibleOccupied = updateVisibilty(row, col);

            if (_currentSeat == empty && visibleOccupied == 0)
            {
                _cachelayout[row] = replaceAtIndex(_cachelayout[row], col, occupied);
                _currentOccupiedCount++;
                seatChanged = true;
            }
            else if (_currentSeat == occupied) 
            { 
                if(visibleOccupied >= 5)
                {
                    _cachelayout[row] = replaceAtIndex(_cachelayout[row], col, empty);
                    _currentOccupiedCount--;
                    seatChanged = true;
                }
            }
            else
                // do nothing to current seat
                _cachelayout[row] = replaceAtIndex(_cachelayout[row], col, _cachelayout[row][col]);


            return seatChanged;
        }

        static bool loopSeatsOnce()
        {
            bool anySeatChanged = false;
            
            _cachelayout = copyLayout(_currentlayout);

            for (int row = 0;row<_currentlayout.Length;row++)
            {
                for(int col=0;col<_currentlayout[0].Length;col++)
                {
                    if (applyRulesToCurrentSeat(row, col))
                        anySeatChanged = true;
                }
            }
            _currentlayout = copyLayout(_cachelayout);

            return anySeatChanged;
        }

        public static void loopSeats()
        {
            bool anySeatsChanged;
            do
            {
                anySeatsChanged = loopSeatsOnce();
                
                _iterations++;
                //showSeats();
            }
            while (anySeatsChanged && _iterations < 1000);
        }

        static string[] copyLayout(string [] source)
        {
            string[] target = new string[source.Length];

            source.CopyTo(target, 0);

            return target;
        }

        public static void getinputfromfile()
        {
            _currentlayout = System.IO.File.ReadAllLines(@"C:\Users\JAH016\Source\Repos\AOC2020\Day11\input.txt");

        }

    }

   

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Code of advent 2020 - Day 11");
            scanner.getinputfromfile();
            scanner.loopSeats();
            var occupiedSeats = scanner.CurrentOccupiedCount;
            var iterations = scanner.Iterations;
            sw.Stop();
            Console.WriteLine($"Occupied seats are {occupiedSeats} after {iterations} iterations.");
            //scanner.showSeats();
            Console.WriteLine("Time elapsed for day11 part1 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
