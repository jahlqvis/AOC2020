using Microsoft.VisualStudio.TestTools.UnitTesting;
using Day12;

namespace Day12Test
{
    [TestClass]
    public class Day12Test
    {
        [TestMethod]
        public void TestRotateL90()
        {
            Position position = new Position();
            position.moveEast(10);
            position.moveNorth(1);

            position.rotateLeft(90);

            Assert.AreEqual(-1, position.getEast());
            Assert.AreEqual(10, position.getNorth());
            
        }

        [TestMethod]
        public void TestRotateL180()
        {
            Position position = new Position();
            position.moveEast(10);
            position.moveNorth(1);

            position.rotateLeft(180);

            Assert.AreEqual(-10, position.getEast());
            Assert.AreEqual(-1, position.getNorth());
            
        }

        [TestMethod]
        public void TestRotateL270()
        {
            Position position = new Position();
            position.moveEast(10);
            position.moveNorth(1);

            position.rotateLeft(270);

            Assert.AreEqual(1, position.getEast());
            Assert.AreEqual(-10, position.getNorth());
            
        }

        [TestMethod]
        public void TestRotateL360()
        {
            Position position = new Position();
            position.moveEast(10);
            position.moveNorth(1);

            position.rotateLeft(360);

            Assert.AreEqual(10, position.getEast());
            Assert.AreEqual(1, position.getNorth());

        }

        [TestMethod]
        public void TestRotateR90()
        {
            Position position = new Position();
            position.moveEast(10);
            position.moveNorth(1);

            position.rotateRight(90);

            Assert.AreEqual(1, position.getEast());
            Assert.AreEqual(-10, position.getNorth());

        }

        [TestMethod]
        public void TestRotateR180()
        {
            Position position = new Position();
            position.moveEast(10);
            position.moveNorth(1);

            position.rotateRight(180);

            Assert.AreEqual(-10, position.getEast());
            Assert.AreEqual(-1, position.getNorth());

        }

        [TestMethod]
        public void TestRotateR270()
        {
            Position position = new Position();
            position.moveEast(10);
            position.moveNorth(1);

            position.rotateRight(270);

            Assert.AreEqual(-1, position.getEast());
            Assert.AreEqual(10, position.getNorth());

        }

        [TestMethod]
        public void TestRotateR360()
        {
            Position position = new Position();
            position.moveEast(10);
            position.moveNorth(1);

            position.rotateRight(360);

            Assert.AreEqual(10, position.getEast());
            Assert.AreEqual(1, position.getNorth());

        }


    }
}
