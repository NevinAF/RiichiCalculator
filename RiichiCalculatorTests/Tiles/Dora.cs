using NUnit.Framework;
using RiichiCalculator;

namespace RiichiCalculatorTests
{
    public class Dora
    {
        [Test]
        public void WindDora()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.EW.DoraTileIndex()), Is.EqualTo(TILE.SW));
            Assert.That(TileExtensions.IndexsFlags(TILE.SW.DoraTileIndex()), Is.EqualTo(TILE.WW));
            Assert.That(TileExtensions.IndexsFlags(TILE.WW.DoraTileIndex()), Is.EqualTo(TILE.NW));
            Assert.That(TileExtensions.IndexsFlags(TILE.NW.DoraTileIndex()), Is.EqualTo(TILE.EW));

            Assert.That(TILE.EW.DoraTileIndex(), Is.EqualTo(TILE.SW.TileIndex()));
            Assert.That(TILE.SW.DoraTileIndex(), Is.EqualTo(TILE.WW.TileIndex()));
            Assert.That(TILE.WW.DoraTileIndex(), Is.EqualTo(TILE.NW.TileIndex()));
            Assert.That(TILE.NW.DoraTileIndex(), Is.EqualTo(TILE.EW.TileIndex()));
        }

        [Test]
        public void DragonDora()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.WD.DoraTileIndex()), Is.EqualTo(TILE.GD));
            Assert.That(TileExtensions.IndexsFlags(TILE.GD.DoraTileIndex()), Is.EqualTo(TILE.RD));
            Assert.That(TileExtensions.IndexsFlags(TILE.RD.DoraTileIndex()), Is.EqualTo(TILE.WD));

            Assert.That(TILE.WD.DoraTileIndex(), Is.EqualTo(TILE.GD.TileIndex()));
            Assert.That(TILE.GD.DoraTileIndex(), Is.EqualTo(TILE.RD.TileIndex()));
            Assert.That(TILE.RD.DoraTileIndex(), Is.EqualTo(TILE.WD.TileIndex()));
        }

        [Test]
        public void ManDora()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.C1.DoraTileIndex()), Is.EqualTo(TILE.C2));
            Assert.That(TileExtensions.IndexsFlags(TILE.C2.DoraTileIndex()), Is.EqualTo(TILE.C3));
            Assert.That(TileExtensions.IndexsFlags(TILE.C3.DoraTileIndex()), Is.EqualTo(TILE.C4));
            Assert.That(TileExtensions.IndexsFlags(TILE.C4.DoraTileIndex()), Is.EqualTo(TILE.C5));
            Assert.That(TileExtensions.IndexsFlags(TILE.C5.DoraTileIndex()), Is.EqualTo(TILE.C6));
            Assert.That(TileExtensions.IndexsFlags(TILE.C6.DoraTileIndex()), Is.EqualTo(TILE.C7));
            Assert.That(TileExtensions.IndexsFlags(TILE.C7.DoraTileIndex()), Is.EqualTo(TILE.C8));
            Assert.That(TileExtensions.IndexsFlags(TILE.C8.DoraTileIndex()), Is.EqualTo(TILE.C9));
            Assert.That(TileExtensions.IndexsFlags(TILE.C9.DoraTileIndex()), Is.EqualTo(TILE.C1));

            Assert.That(TILE.C1.DoraTileIndex(), Is.EqualTo(TILE.C2.TileIndex()));
            Assert.That(TILE.C2.DoraTileIndex(), Is.EqualTo(TILE.C3.TileIndex()));
            Assert.That(TILE.C3.DoraTileIndex(), Is.EqualTo(TILE.C4.TileIndex()));
            Assert.That(TILE.C4.DoraTileIndex(), Is.EqualTo(TILE.C5.TileIndex()));
            Assert.That(TILE.C5.DoraTileIndex(), Is.EqualTo(TILE.C6.TileIndex()));
            Assert.That(TILE.C6.DoraTileIndex(), Is.EqualTo(TILE.C7.TileIndex()));
            Assert.That(TILE.C7.DoraTileIndex(), Is.EqualTo(TILE.C8.TileIndex()));
            Assert.That(TILE.C8.DoraTileIndex(), Is.EqualTo(TILE.C9.TileIndex()));
            Assert.That(TILE.C9.DoraTileIndex(), Is.EqualTo(TILE.C1.TileIndex()));
        }

        [Test]
        public void PinDora()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.D1.DoraTileIndex()), Is.EqualTo(TILE.D2));
            Assert.That(TileExtensions.IndexsFlags(TILE.D2.DoraTileIndex()), Is.EqualTo(TILE.D3));
            Assert.That(TileExtensions.IndexsFlags(TILE.D3.DoraTileIndex()), Is.EqualTo(TILE.D4));
            Assert.That(TileExtensions.IndexsFlags(TILE.D4.DoraTileIndex()), Is.EqualTo(TILE.D5));
            Assert.That(TileExtensions.IndexsFlags(TILE.D5.DoraTileIndex()), Is.EqualTo(TILE.D6));
            Assert.That(TileExtensions.IndexsFlags(TILE.D6.DoraTileIndex()), Is.EqualTo(TILE.D7));
            Assert.That(TileExtensions.IndexsFlags(TILE.D7.DoraTileIndex()), Is.EqualTo(TILE.D8));
            Assert.That(TileExtensions.IndexsFlags(TILE.D8.DoraTileIndex()), Is.EqualTo(TILE.D9));
            Assert.That(TileExtensions.IndexsFlags(TILE.D9.DoraTileIndex()), Is.EqualTo(TILE.D1));

            Assert.That(TILE.D1.DoraTileIndex(), Is.EqualTo(TILE.D2.TileIndex()));
            Assert.That(TILE.D2.DoraTileIndex(), Is.EqualTo(TILE.D3.TileIndex()));
            Assert.That(TILE.D3.DoraTileIndex(), Is.EqualTo(TILE.D4.TileIndex()));
            Assert.That(TILE.D4.DoraTileIndex(), Is.EqualTo(TILE.D5.TileIndex()));
            Assert.That(TILE.D5.DoraTileIndex(), Is.EqualTo(TILE.D6.TileIndex()));
            Assert.That(TILE.D6.DoraTileIndex(), Is.EqualTo(TILE.D7.TileIndex()));
            Assert.That(TILE.D7.DoraTileIndex(), Is.EqualTo(TILE.D8.TileIndex()));
            Assert.That(TILE.D8.DoraTileIndex(), Is.EqualTo(TILE.D9.TileIndex()));
            Assert.That(TILE.D9.DoraTileIndex(), Is.EqualTo(TILE.D1.TileIndex()));
        }

        [Test]
        public void SouDora()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.B1.DoraTileIndex()), Is.EqualTo(TILE.B2));
            Assert.That(TileExtensions.IndexsFlags(TILE.B2.DoraTileIndex()), Is.EqualTo(TILE.B3));
            Assert.That(TileExtensions.IndexsFlags(TILE.B3.DoraTileIndex()), Is.EqualTo(TILE.B4));
            Assert.That(TileExtensions.IndexsFlags(TILE.B4.DoraTileIndex()), Is.EqualTo(TILE.B5));
            Assert.That(TileExtensions.IndexsFlags(TILE.B5.DoraTileIndex()), Is.EqualTo(TILE.B6));
            Assert.That(TileExtensions.IndexsFlags(TILE.B6.DoraTileIndex()), Is.EqualTo(TILE.B7));
            Assert.That(TileExtensions.IndexsFlags(TILE.B7.DoraTileIndex()), Is.EqualTo(TILE.B8));
            Assert.That(TileExtensions.IndexsFlags(TILE.B8.DoraTileIndex()), Is.EqualTo(TILE.B9));
            Assert.That(TileExtensions.IndexsFlags(TILE.B9.DoraTileIndex()), Is.EqualTo(TILE.B1));

            Assert.That(TILE.B1.DoraTileIndex(), Is.EqualTo(TILE.B2.TileIndex()));
            Assert.That(TILE.B2.DoraTileIndex(), Is.EqualTo(TILE.B3.TileIndex()));
            Assert.That(TILE.B3.DoraTileIndex(), Is.EqualTo(TILE.B4.TileIndex()));
            Assert.That(TILE.B4.DoraTileIndex(), Is.EqualTo(TILE.B5.TileIndex()));
            Assert.That(TILE.B5.DoraTileIndex(), Is.EqualTo(TILE.B6.TileIndex()));
            Assert.That(TILE.B6.DoraTileIndex(), Is.EqualTo(TILE.B7.TileIndex()));
            Assert.That(TILE.B7.DoraTileIndex(), Is.EqualTo(TILE.B8.TileIndex()));
            Assert.That(TILE.B8.DoraTileIndex(), Is.EqualTo(TILE.B9.TileIndex()));
            Assert.That(TILE.B9.DoraTileIndex(), Is.EqualTo(TILE.B1.TileIndex()));
        }
    }
}