using NUnit.Framework;
using RiichiCalculator;

namespace RiichiCalculatorTests
{
    public class Indexing
    {
        [Test]
        public void Winds()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.EW.TileIndex()), Is.EqualTo(TILE.EW));
            Assert.That(TileExtensions.IndexsFlags(TILE.SW.TileIndex()), Is.EqualTo(TILE.SW));
            Assert.That(TileExtensions.IndexsFlags(TILE.WW.TileIndex()), Is.EqualTo(TILE.WW));
            Assert.That(TileExtensions.IndexsFlags(TILE.NW.TileIndex()), Is.EqualTo(TILE.NW));
        }

        [Test]
        public void Dragons()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.WD.TileIndex()), Is.EqualTo(TILE.WD));
            Assert.That(TileExtensions.IndexsFlags(TILE.GD.TileIndex()), Is.EqualTo(TILE.GD));
            Assert.That(TileExtensions.IndexsFlags(TILE.RD.TileIndex()), Is.EqualTo(TILE.RD));
        }

        [Test]
        public void Man()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.M1.TileIndex()), Is.EqualTo(TILE.M1));
            Assert.That(TileExtensions.IndexsFlags(TILE.M2.TileIndex()), Is.EqualTo(TILE.M2));
            Assert.That(TileExtensions.IndexsFlags(TILE.M3.TileIndex()), Is.EqualTo(TILE.M3));
            Assert.That(TileExtensions.IndexsFlags(TILE.M4.TileIndex()), Is.EqualTo(TILE.M4));
            Assert.That(TileExtensions.IndexsFlags(TILE.M5.TileIndex()), Is.EqualTo(TILE.M5));
            Assert.That(TileExtensions.IndexsFlags(TILE.M6.TileIndex()), Is.EqualTo(TILE.M6));
            Assert.That(TileExtensions.IndexsFlags(TILE.M7.TileIndex()), Is.EqualTo(TILE.M7));
            Assert.That(TileExtensions.IndexsFlags(TILE.M8.TileIndex()), Is.EqualTo(TILE.M8));
            Assert.That(TileExtensions.IndexsFlags(TILE.M9.TileIndex()), Is.EqualTo(TILE.M9));
        }

        [Test]
        public void Pin()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.P1.TileIndex()), Is.EqualTo(TILE.P1));
            Assert.That(TileExtensions.IndexsFlags(TILE.P2.TileIndex()), Is.EqualTo(TILE.P2));
            Assert.That(TileExtensions.IndexsFlags(TILE.P3.TileIndex()), Is.EqualTo(TILE.P3));
            Assert.That(TileExtensions.IndexsFlags(TILE.P4.TileIndex()), Is.EqualTo(TILE.P4));
            Assert.That(TileExtensions.IndexsFlags(TILE.P5.TileIndex()), Is.EqualTo(TILE.P5));
            Assert.That(TileExtensions.IndexsFlags(TILE.P6.TileIndex()), Is.EqualTo(TILE.P6));
            Assert.That(TileExtensions.IndexsFlags(TILE.P7.TileIndex()), Is.EqualTo(TILE.P7));
            Assert.That(TileExtensions.IndexsFlags(TILE.P8.TileIndex()), Is.EqualTo(TILE.P8));
            Assert.That(TileExtensions.IndexsFlags(TILE.P9.TileIndex()), Is.EqualTo(TILE.P9));
        }

        [Test]
        public void Sou()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.S1.TileIndex()), Is.EqualTo(TILE.S1));
            Assert.That(TileExtensions.IndexsFlags(TILE.S2.TileIndex()), Is.EqualTo(TILE.S2));
            Assert.That(TileExtensions.IndexsFlags(TILE.S3.TileIndex()), Is.EqualTo(TILE.S3));
            Assert.That(TileExtensions.IndexsFlags(TILE.S4.TileIndex()), Is.EqualTo(TILE.S4));
            Assert.That(TileExtensions.IndexsFlags(TILE.S5.TileIndex()), Is.EqualTo(TILE.S5));
            Assert.That(TileExtensions.IndexsFlags(TILE.S6.TileIndex()), Is.EqualTo(TILE.S6));
            Assert.That(TileExtensions.IndexsFlags(TILE.S7.TileIndex()), Is.EqualTo(TILE.S7));
            Assert.That(TileExtensions.IndexsFlags(TILE.S8.TileIndex()), Is.EqualTo(TILE.S8));
            Assert.That(TileExtensions.IndexsFlags(TILE.S9.TileIndex()), Is.EqualTo(TILE.S9));
        }
    }
}