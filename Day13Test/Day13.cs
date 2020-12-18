using Microsoft.VisualStudio.TestTools.UnitTesting;
using Day13;

namespace Day13Test
{
    [TestClass]
    public class Day13Test
    {
        static string[] testnote = new string[]
        {
            "1068781",
            "7,13,x,x,59,x,31,19"
        };

        static string[] testnote2 = new string[]
        {
            "3417",
            "17,x,13,19"
        };

        static string[] testnote3 = new string[]
        {
            "754018",
            "67,7,59,61"
        };

        static string[] testnote4 = new string[]
        {
            "779210",
            "67,x,7,59,61"
        };


        static string[] testnote5 = new string[]
        {
            "1261476",
            "67,7,x,59,61"
        };

        static string[] testnote6 = new string[]
        {
            "1202161486",
            "1789,37,47,1889"
        };

        public static string[] testnote7 = new string[]
       {
            "48593386",
            "29,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,661,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,23,x,x,x,x,x,x,x,521"
       };


        [TestMethod]
        public void TestNote1()
        {
            busschedules.init(testnote);
            ulong res = busschedules.startsearch();

            Assert.AreEqual(ulong.Parse(testnote[0]), res);
        }

        [TestMethod]
        public void TestNote2()
        {
            busschedules.init(testnote2);
            ulong res = busschedules.startsearch();

            Assert.AreEqual(ulong.Parse(testnote2[0]), res);
        }

        [TestMethod]
        public void TestNote3()
        {
            busschedules.init(testnote3);
            ulong res = busschedules.startsearch();

            Assert.AreEqual(ulong.Parse(testnote3[0]), res);
        }

        [TestMethod]
        public void TestNote4()
        {
            busschedules.init(testnote4);
            ulong res = busschedules.startsearch();

            Assert.AreEqual(ulong.Parse(testnote4[0]), res);
        }

        [TestMethod]
        public void TestNote5()
        {
            busschedules.init(testnote5);
            ulong res = busschedules.startsearch();

            Assert.AreEqual(ulong.Parse(testnote5[0]), res);
        }

        [TestMethod]
        public void TestNote6()
        {
            busschedules.init(testnote6);
            ulong res = busschedules.startsearch();

            Assert.AreEqual(ulong.Parse(testnote6[0]), res);
        }

        [TestMethod]
        public void TestNote7()
        {
            busschedules.init(testnote7);
            ulong res = busschedules.startsearch();

            Assert.AreEqual(ulong.Parse(testnote7[0]), res);
        }



    }
}
