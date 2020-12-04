using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day4
{
    static class input
    {
        
        public static string[] batch = new string[]
        {
            "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
            "byr:1937 iyr:2017 cid:147 hgt:183cm",
            "",
            "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
            "hcl:#cfa07d byr:1929",
            "",
            "hcl:#ae17e1 iyr:2013",
            "eyr:2024",
            "ecl:brn pid:760753108 byr:1931",
            "hgt:179cm",
            "",
            "hcl:#cfa07d eyr:2025 pid:166559648",
            "iyr:2011 ecl:brn hgt:59in"
        };

        public static string[] invalid4batch = new string[]
        {
            "eyr:1972 cid:100",
            "hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
            "",
            "iyr:2019",
            "hcl:#602927 eyr:1967 hgt:170cm",
            "ecl:grn pid:012533040 byr:1946",
            "",
            "hcl:dab227 iyr:2012",
            "ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
            "",
            "hgt:59cm ecl:zzz",
            "eyr:2038 hcl:74454a iyr:2023",
            "pid:3556412378 byr:2007"
        };

        public static string[] valid4batch = new string[]
        {
            "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
            "hcl:#623a2f",
            "",
            "eyr:2029 ecl:blu cid:129 byr:1989",
            "iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
            "",
            "hcl:#888785",
            "hgt:164cm byr:2001 iyr:2015 cid:88",
            "pid:545766238 ecl:hzl",
            "eyr:2022",
            "",
            "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
        };
           

        static public void getinputfromfile()
        {
            batch = System.IO.File.ReadAllLines(@"C:\Users\JAH016\Source\Repos\AOC2020\Day4\input.txt");

        }

    }

    class password
    {
        string byr; // (Birth Year)
        string iyr; // (Issue Year)
        string eyr; // (Expiration Year)
        string hgt; // (Height)
        string hcl; // (Hair Color)
        string ecl; // (Eye Color)
        string pid; //  (Passport ID)
        string cid; // (Country ID)

        public bool isValid;

        public password(string row)
        {
            byr = string.Empty;
            iyr = string.Empty;
            eyr = string.Empty;
            hgt = string.Empty;
            hcl = string.Empty;
            ecl = string.Empty;
            pid = string.Empty;
            cid = string.Empty;
            isValid = false;

            string[] passwordFields = row.Split(" ");


            int i;
            foreach (var f in passwordFields)
            {
                if (f.Contains("byr"))
                    byr = getvalue(f);
                else if (f.Contains("iyr"))
                    iyr = getvalue(f);
                else if (f.Contains("eyr"))
                    eyr = getvalue(f);
                else if (f.Contains("hgt"))
                    hgt = getvalue(f);
                else if (f.Contains("hcl"))
                    hcl = getvalue(f);
                else if (f.Contains("ecl"))
                    ecl = getvalue(f);
                else if (f.Contains("pid"))
                    pid = getvalue(f);
                else if (f.Contains("cid"))
                    cid = getvalue(f);
                else
                    ;

            }

            isValid = Verify();
        }

        private string getvalue(string fieldStr)
        {
            string[] parts = fieldStr.Split(':');
            return parts[1];
        }

        bool Verify()
        {
            //byr(Birth Year) - four digits; at least 1920 and at most 2002.
            //iyr(Issue Year) - four digits; at least 2010 and at most 2020.
            //eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
            //hgt(Height) - a number followed by either cm or in:
            //If cm, the number must be at least 150 and at most 193.
            //If in, the number must be at least 59 and at most 76.
            //hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            //ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            //pid(Passport ID) - a nine - digit number, including leading zeroes.
            //cid(Country ID) - ignored, missing or not.

            if (!byrRule())
                return false;
            if (!iyrRule())
                return false;
            if (!eyrRule())
                return false;
            if (!hgtRule())
                return false;
            if (!hclRule())
                return false;
            if (!eclRule())
                return false;
            if (!pidRule())
                return false;

            return true;
        }

        bool byrRule()
        {
            if(byr.Length != 4) 
                return false;

            if (int.Parse(byr) < 1920 || int.Parse(byr) > 2002)
                return false;
            return true;
        }

        bool iyrRule()
        {
            if (iyr.Length != 4)
                return false;

            if (int.Parse(iyr) < 2010 || int.Parse(iyr) > 2020)
                return false;
            return true;
        }

        bool eyrRule()
        {
            if (eyr.Length != 4)
                return false;

            if (int.Parse(eyr) < 2020 || int.Parse(eyr) > 2030)
                return false;
            return true;
        }

        bool hgtRule()
        {
            if (hgt.Length < 4 || hgt.Length > 5) // valid length if in or cm
                return false;

            string unit = hgt.Substring(hgt.Length-2, 2);
            string value; 

            bool res = false;
            if(unit == "cm")
            {
                if (hgt.Length < 5)
                    return false;

                value = hgt.Substring(0, 3);
                
                if (int.Parse(value) >= 150 && int.Parse(value) <= 193)
                    res = true;
            }
            if (unit == "in")
            {
                if (hgt.Length > 4)
                    return false;

                value = hgt.Substring(0, 2);

                if (int.Parse(value) >= 59 && int.Parse(value) <= 76)
                    return true;
            }
            return res;
        }

        bool hclRule()
        {
            if (hcl.Length != 7)
                return false;

            if (hcl[0] != '#')
                return false;

            string hclValue = hcl.Substring(1, 6);
            int value=0;
            bool res = false;
            try
            {
                value = Convert.ToInt32(hclValue, 16);
            }
            catch(Exception e)
            {
                res = false;
            }

            if (value >= 0 && value <= 16777215)
                res = true;
            
            return res;
        }

        bool eclRule()
        {
            string[] validValues = new string[]
            {
                "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
            };

            foreach(var v in validValues)
            {
                if (v == ecl)
                    return true;
            }
            return false;
        }

        bool pidRule()
        {
            if (pid.Length != 9)
                return false;

            int pidValue = 0;
            if (!int.TryParse(pid, out pidValue))
                return false;

            return true;
        }

    }

    static class scanner
    {
        static List<password> scannedPasswords = new List<password>();

        public static void parseinput()
        {
            int i = 0;
            string passwordStr = "";
            do
            {
                if(input.batch[i] == "")
                {
                    scannedPasswords.Add(new password(passwordStr));
                    passwordStr = "";
                }
                else if(passwordStr == "")
                {
                    passwordStr += input.batch[i];
                }
                else
                {
                    passwordStr += " " + input.batch[i];
                }
                i++;
            } while (i < input.batch.Length);
            
            scannedPasswords.Add(new password(passwordStr));
        }

        public static int getvalidpasswords()
        {
            int i = 0;
            foreach (var pw in scannedPasswords)
                if (pw.isValid) i++;
            return i;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Code of advent 2020 - Day 4");
            sw.Start();
            input.getinputfromfile();
            scanner.parseinput();
            var res = scanner.getvalidpasswords();
            sw.Stop();
            Console.WriteLine(res);
            Console.WriteLine("Time elapsed for day4 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }
    }
}
