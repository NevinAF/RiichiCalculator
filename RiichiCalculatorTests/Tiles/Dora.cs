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
            Assert.That(TileExtensions.IndexsFlags(TILE.M1.DoraTileIndex()), Is.EqualTo(TILE.M2));
            Assert.That(TileExtensions.IndexsFlags(TILE.M2.DoraTileIndex()), Is.EqualTo(TILE.M3));
            Assert.That(TileExtensions.IndexsFlags(TILE.M3.DoraTileIndex()), Is.EqualTo(TILE.M4));
            Assert.That(TileExtensions.IndexsFlags(TILE.M4.DoraTileIndex()), Is.EqualTo(TILE.M5));
            Assert.That(TileExtensions.IndexsFlags(TILE.M5.DoraTileIndex()), Is.EqualTo(TILE.M6));
            Assert.That(TileExtensions.IndexsFlags(TILE.M6.DoraTileIndex()), Is.EqualTo(TILE.M7));
            Assert.That(TileExtensions.IndexsFlags(TILE.M7.DoraTileIndex()), Is.EqualTo(TILE.M8));
            Assert.That(TileExtensions.IndexsFlags(TILE.M8.DoraTileIndex()), Is.EqualTo(TILE.M9));
            Assert.That(TileExtensions.IndexsFlags(TILE.M9.DoraTileIndex()), Is.EqualTo(TILE.M1));

            Assert.That(TILE.M1.DoraTileIndex(), Is.EqualTo(TILE.M2.TileIndex()));
            Assert.That(TILE.M2.DoraTileIndex(), Is.EqualTo(TILE.M3.TileIndex()));
            Assert.That(TILE.M3.DoraTileIndex(), Is.EqualTo(TILE.M4.TileIndex()));
            Assert.That(TILE.M4.DoraTileIndex(), Is.EqualTo(TILE.M5.TileIndex()));
            Assert.That(TILE.M5.DoraTileIndex(), Is.EqualTo(TILE.M6.TileIndex()));
            Assert.That(TILE.M6.DoraTileIndex(), Is.EqualTo(TILE.M7.TileIndex()));
            Assert.That(TILE.M7.DoraTileIndex(), Is.EqualTo(TILE.M8.TileIndex()));
            Assert.That(TILE.M8.DoraTileIndex(), Is.EqualTo(TILE.M9.TileIndex()));
            Assert.That(TILE.M9.DoraTileIndex(), Is.EqualTo(TILE.M1.TileIndex()));
        }

        [Test]
        public void PinDora()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.P1.DoraTileIndex()), Is.EqualTo(TILE.P2));
            Assert.That(TileExtensions.IndexsFlags(TILE.P2.DoraTileIndex()), Is.EqualTo(TILE.P3));
            Assert.That(TileExtensions.IndexsFlags(TILE.P3.DoraTileIndex()), Is.EqualTo(TILE.P4));
            Assert.That(TileExtensions.IndexsFlags(TILE.P4.DoraTileIndex()), Is.EqualTo(TILE.P5));
            Assert.That(TileExtensions.IndexsFlags(TILE.P5.DoraTileIndex()), Is.EqualTo(TILE.P6));
            Assert.That(TileExtensions.IndexsFlags(TILE.P6.DoraTileIndex()), Is.EqualTo(TILE.P7));
            Assert.That(TileExtensions.IndexsFlags(TILE.P7.DoraTileIndex()), Is.EqualTo(TILE.P8));
            Assert.That(TileExtensions.IndexsFlags(TILE.P8.DoraTileIndex()), Is.EqualTo(TILE.P9));
            Assert.That(TileExtensions.IndexsFlags(TILE.P9.DoraTileIndex()), Is.EqualTo(TILE.P1));

            Assert.That(TILE.P1.DoraTileIndex(), Is.EqualTo(TILE.P2.TileIndex()));
            Assert.That(TILE.P2.DoraTileIndex(), Is.EqualTo(TILE.P3.TileIndex()));
            Assert.That(TILE.P3.DoraTileIndex(), Is.EqualTo(TILE.P4.TileIndex()));
            Assert.That(TILE.P4.DoraTileIndex(), Is.EqualTo(TILE.P5.TileIndex()));
            Assert.That(TILE.P5.DoraTileIndex(), Is.EqualTo(TILE.P6.TileIndex()));
            Assert.That(TILE.P6.DoraTileIndex(), Is.EqualTo(TILE.P7.TileIndex()));
            Assert.That(TILE.P7.DoraTileIndex(), Is.EqualTo(TILE.P8.TileIndex()));
            Assert.That(TILE.P8.DoraTileIndex(), Is.EqualTo(TILE.P9.TileIndex()));
            Assert.That(TILE.P9.DoraTileIndex(), Is.EqualTo(TILE.P1.TileIndex()));
        }

        [Test]
        public void SouDora()
        {
            Assert.That(TileExtensions.IndexsFlags(TILE.S1.DoraTileIndex()), Is.EqualTo(TILE.S2));
            Assert.That(TileExtensions.IndexsFlags(TILE.S2.DoraTileIndex()), Is.EqualTo(TILE.S3));
            Assert.That(TileExtensions.IndexsFlags(TILE.S3.DoraTileIndex()), Is.EqualTo(TILE.S4));
            Assert.That(TileExtensions.IndexsFlags(TILE.S4.DoraTileIndex()), Is.EqualTo(TILE.S5));
            Assert.That(TileExtensions.IndexsFlags(TILE.S5.DoraTileIndex()), Is.EqualTo(TILE.S6));
            Assert.That(TileExtensions.IndexsFlags(TILE.S6.DoraTileIndex()), Is.EqualTo(TILE.S7));
            Assert.That(TileExtensions.IndexsFlags(TILE.S7.DoraTileIndex()), Is.EqualTo(TILE.S8));
            Assert.That(TileExtensions.IndexsFlags(TILE.S8.DoraTileIndex()), Is.EqualTo(TILE.S9));
            Assert.That(TileExtensions.IndexsFlags(TILE.S9.DoraTileIndex()), Is.EqualTo(TILE.S1));

            Assert.That(TILE.S1.DoraTileIndex(), Is.EqualTo(TILE.S2.TileIndex()));
            Assert.That(TILE.S2.DoraTileIndex(), Is.EqualTo(TILE.S3.TileIndex()));
            Assert.That(TILE.S3.DoraTileIndex(), Is.EqualTo(TILE.S4.TileIndex()));
            Assert.That(TILE.S4.DoraTileIndex(), Is.EqualTo(TILE.S5.TileIndex()));
            Assert.That(TILE.S5.DoraTileIndex(), Is.EqualTo(TILE.S6.TileIndex()));
            Assert.That(TILE.S6.DoraTileIndex(), Is.EqualTo(TILE.S7.TileIndex()));
            Assert.That(TILE.S7.DoraTileIndex(), Is.EqualTo(TILE.S8.TileIndex()));
            Assert.That(TILE.S8.DoraTileIndex(), Is.EqualTo(TILE.S9.TileIndex()));
            Assert.That(TILE.S9.DoraTileIndex(), Is.EqualTo(TILE.S1.TileIndex()));
        }
    }
}