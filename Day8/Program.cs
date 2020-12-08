using System;
using System.Diagnostics;

namespace Day8
{
    static class gameconsole
    {
        static int accu = 0;
        
        public static string[] testcode = new string[]
        {
            "nop +0",
            "acc +1",
            "jmp +4",
            "acc +3",
            "jmp -3",
            "acc -99",
            "acc +1",
            "jmp -4",
            "acc +6"
        };

        public static string[] code;

        private static MemoryOp[] memory;

        private static MemoryOp[] memory2;

        public static int Accu { get => accu; }

        public static void getinputfromfile()
        {
            code = System.IO.File.ReadAllLines(@"C:\Users\JAH016\Source\Repos\AOC2020\Day8\input.txt");

        }

        private struct MemoryOp 
        {
            string _op;
            int _value;
            bool _changed;
            int _called;

            public MemoryOp(string op, int value, bool changed, int called) { _op = op; _value = value;  _changed = changed;  _called = called; }

            public string Op 
            {
                get => _op;
                set => _op = value;
            }
            public int Called { get => _called; set => _called = value; }
            public bool Changed { get => _changed; set => _changed = value; }
            public int Value { get => _value; }
        }

        private static bool change_nopjmp(ref MemoryOp mo)
        {
            if (mo.Op == "jmp")
            {
                mo.Op = "nop";
                mo.Changed = true;
                return true;
            }
            else if (mo.Op == "nop")
            {
                mo.Op = "jmp";
                mo.Changed = true;
                return true;
            }
            else
                return false;
        }

        public static void load_code_to_memory(string[] code)
        {
            int code_size = code.Length;
            memory = new MemoryOp[code_size];

            int i = 0;
            foreach(var c in code)
            {
                memory[i] = decode(c);
                i++;
            }

        }


        public static void run()
        {
            int program_counter = 0;
            int code_size = memory.Length;

            do
            {
                program_counter += execute(ref memory[program_counter]);

                if (program_counter < 0 || program_counter >= code_size)
                    throw new ArgumentException("Illegal program counter value");

                Console.WriteLine($"accu: {accu}");

            }
            while (memory[program_counter].Called < 1);
            
        }

        public static bool run2(int run_num, out int changed_pc)
        {
            int program_counter = 0;
            int previous_program_counter = -1;
            int code_size = memory.Length;
            changed_pc = -1;
            accu = 0;
            
            do
            {
                if (changed_pc < 0 && !memory[program_counter].Changed)
                    if(change_nopjmp(ref memory[program_counter]))
                        changed_pc = program_counter;

                program_counter += execute(ref memory[program_counter]);

                if (program_counter == previous_program_counter)
                    break;

                if (program_counter < 0 || program_counter >= code_size)
                    throw new ArgumentException("Illegal program counter value");

                if (program_counter == code_size - 1 && changed_pc >= 0)
                    return true;

                previous_program_counter = program_counter;
            }
            while (memory[program_counter].Called < run_num);

            if(changed_pc > 0)
            {
                // change back
                change_nopjmp(ref memory[changed_pc]);
            }
            
            return false;
        }

        static MemoryOp decode(string row)
        {
            string[] r = row.Split(' ');

            string opcode = r[0];
            int value = 0;
            if (!int.TryParse(r[1], out value))
                throw new ArgumentException($"Could not parse opcode value {r[1]}");

            return new MemoryOp(opcode, value, false, 0);
        }

        static int execute(ref MemoryOp mo)
        {
            mo.Called++;
            switch (mo.Op)
            {
                case "nop":
                    return 1;        
                
                case "acc":
                    accu += mo.Value;
                    return 1;

                case "jmp":
                    return mo.Value;

                default:
                    throw new ArgumentException($"Unknown opcode {mo.Op}");
            }

        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Code of advent 2020 - Day 8");
            sw.Start();
            gameconsole.getinputfromfile();
            gameconsole.load_code_to_memory(gameconsole.code);
            int pc = 0;
            int run_time = 0;
            while (!gameconsole.run2(run_time, out pc))
            {
                Console.WriteLine($"{pc}");
                run_time++;
            };
            Console.WriteLine($"Program counter: {pc}");
            Console.WriteLine($"Accumulator: {gameconsole.Accu}");
            sw.Stop();
            Console.WriteLine("Time elapsed for day8 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
