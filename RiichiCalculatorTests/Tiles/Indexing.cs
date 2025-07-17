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
            Assert.That(TileExtensions.IndexsFlags(TILE.C1.TileIndex()), Is.EqualTo(TILE.C1));
            Assert.That(TileExtensions.IndexsFlags(TILE.C2.TileIndex()), Is.EqualTo(TILE.C2));
            Assert.That(TileExtensions.IndexsFlags(TILE.C3.TileIndex()), Is.EqualTo(TILE.C3));
            Assert.That(TileExtensions.IndexsFlags(TILE.C4.TileIndex()), Is.EqualTo(TILE.C4));
            Assert.That(TileExtensions.IndexsFlags(TILE.C5.TileIndex()), Is.EqualTo(TILE.C5));
            Assert.That(TileExtensions.IndexsFlags(TILE.C6.TileIndex()), Is.EqualTo(TILE.C6));
            Assert.That(TileExtensions.IndexsFlags(TILE.C7.TileIndex()), Is.EqualTo(TILE.C7));
            Assert.That(TileExtensions.IndexsFlags(TILE.C8.TileIndex()), Is.EqualTo(TILE.C8));
            Assert.That(TileExtensions.IndexsFlags(TILE.C9.TileIndex()), Is.EqualTo(TILE.C9));
        }

        [Test]
        public void Pin()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.D1.TileIndex()), Is.EqualTo(TILE.D1));
            Assert.That(TileExtensions.IndexsFlags(TILE.D2.TileIndex()), Is.EqualTo(TILE.D2));
            Assert.That(TileExtensions.IndexsFlags(TILE.D3.TileIndex()), Is.EqualTo(TILE.D3));
            Assert.That(TileExtensions.IndexsFlags(TILE.D4.TileIndex()), Is.EqualTo(TILE.D4));
            Assert.That(TileExtensions.IndexsFlags(TILE.D5.TileIndex()), Is.EqualTo(TILE.D5));
            Assert.That(TileExtensions.IndexsFlags(TILE.D6.TileIndex()), Is.EqualTo(TILE.D6));
            Assert.That(TileExtensions.IndexsFlags(TILE.D7.TileIndex()), Is.EqualTo(TILE.D7));
            Assert.That(TileExtensions.IndexsFlags(TILE.D8.TileIndex()), Is.EqualTo(TILE.D8));
            Assert.That(TileExtensions.IndexsFlags(TILE.D9.TileIndex()), Is.EqualTo(TILE.D9));
        }

        [Test]
        public void Sou()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.B1.TileIndex()), Is.EqualTo(TILE.B1));
            Assert.That(TileExtensions.IndexsFlags(TILE.B2.TileIndex()), Is.EqualTo(TILE.B2));
            Assert.That(TileExtensions.IndexsFlags(TILE.B3.TileIndex()), Is.EqualTo(TILE.B3));
            Assert.That(TileExtensions.IndexsFlags(TILE.B4.TileIndex()), Is.EqualTo(TILE.B4));
            Assert.That(TileExtensions.IndexsFlags(TILE.B5.TileIndex()), Is.EqualTo(TILE.B5));
            Assert.That(TileExtensions.IndexsFlags(TILE.B6.TileIndex()), Is.EqualTo(TILE.B6));
            Assert.That(TileExtensions.IndexsFlags(TILE.B7.TileIndex()), Is.EqualTo(TILE.B7));
            Assert.That(TileExtensions.IndexsFlags(TILE.B8.TileIndex()), Is.EqualTo(TILE.B8));
            Assert.That(TileExtensions.IndexsFlags(TILE.B9.TileIndex()), Is.EqualTo(TILE.B9));
        }
    }
}