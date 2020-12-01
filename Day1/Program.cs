﻿using System;
using System.Diagnostics;

namespace Day1
{
    
    class Program
    {
        static int[] input = new int[]
        {
            1934,
            1702,
            1571,
            1737,
            1977,
            1531,
            1428,
            1695,
            1794,
            1101,
              13,
            1164,
            1235,
            1289,
            1736,
            1814,
            1363,
            1147,
            1111,
            1431,
            1765,
            1515,
            1184,
            1036,
            1803,
            1791,
            1638,
            1809,
            1283,
            1980,
            1854,
            1878,
            1574,
            1352,
            1151,
            730 ,
            1581,
            1990,
            1919,
            2003,
            1538,
            1663,
            1735,
            1772,
            1830,
            1152,
            1022,
            1774,
            1544,
            1551,
            1835,
            1383,
            1614,
            1396,
            1715,
            1530,
            295 ,
            1208,
            1978,
            1104,
            1691,
            1176,
            1183,
            1909,
            1192,
            1535,
            1924,
            1268,
            1969,
            1954,
            1760,
            1077,
            1734,
            1371,
            1676,
            1933,
            1400,
            1928,
            1982,
            1541,
            1106,
            1248,
            1346,
            1782,
            1142,
            1849,
            1798,
            1362,
            1379,
            1886,
            1265,
            1226,
            1751,
            1575,
            1027,
            1710,
            1601,
            1205,
            1922,
            1452,
            1206,
            1263,
            2000,
            1957,
            1951,
            1834,
            1533,
            1149,
            1245,
            1564,
            1182,
            1237,
            1013,
            1254,
            1895,
            1504,
            1480,
            1556,
            1821,
            1589,
            1864,
            1573,
            1698,
            1927,
            1434,
             516,
            1722,
            1360,
            1940,
            1212,
            1329,
            1675,
            1812,
            1917,
            1302,
            1604,
            1336,
            1233,
            1405,
            1179,
            1169,
            1081,
            1941,
            1553,
            1236,
            1824,
            1923,
            1938,
            1475,
            1446,
            1545,
            1853,
            1664,
            317 ,
            1489,
            1884,
            1743,
            1621,
            1128,
            1474,
            1505,
             394,
            1387,
            1509,
            1627,
            1914,
            1913,
            1949,
            1843,
            1847,
            1882,
            1486,
            1082,
            1802,
            1645,
            1690,
            1629,
            1377,
            2004,
            1044,
            1191,
            1014,
            1857,
            1813,
            1572,
            1055,
            1002,
            1721,
            1273,
            1417,
            1968,
            1888,
            1863,
            1278,
            1141,
            1964,
            1259,
            1823,
            1181,
            1779
        };

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Code of advent 2020 - Day 1");
            sw.Start();
            var res = Find3(2020);
            sw.Stop();
            Console.WriteLine(res[0] * res[1] * res[2]);
            Console.WriteLine("Time elapsed for day1 part2 .NET 5 (ms): {0}", sw.Elapsed.TotalMilliseconds);
        }

        static int[] Find2(int result)
        {
            int[] found = new int[2] { -1, -1 };
            for(int i=0;i<Program.input.Length;i++)
            {
                for (int j = 0; j < Program.input.Length; j++)
                {
                    if(Program.input[i] + Program.input[j] == result)
                    {
                        found[0] = Program.input[i];
                        found[1] = Program.input[j];
                        return found;
                    }
                }
            }
            return found;
        }

        static int[] Find3(int result)
        {
            int[] found = new int[3] { -1, -1, -1 };
            for (int i = 0; i < Program.input.Length; i++)
            {
                for (int j = 0; j < Program.input.Length; j++)
                {
                    for (int k = 0; k < Program.input.Length; k++)
                    {
                        if (Program.input[i] + Program.input[j] + Program.input[k] == result)
                        {
                            found[0] = Program.input[i];
                            found[1] = Program.input[j];
                            found[2] = Program.input[k];
                            return found;
                        }
                    }
                }
            }
            return found;
        }
    }
}
