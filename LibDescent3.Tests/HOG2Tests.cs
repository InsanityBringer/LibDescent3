using System.IO;
using NUnit.Framework;
using LibDescent3.Tests;

namespace LibDescent3.Tests
{
    class HOG2Tests
    {
        [Test]
        public void TestReadHOG2File()
        {
            HOG2File hogFile = new HOG2File(TestUtils.GetResourceStream("EXTRA13.HOG"));
            Assert.AreEqual(25, hogFile.NumLumps);
        }

        [Test]
        public void TestHOG2Headers()
        {
            HOG2File hogFile = new HOG2File(TestUtils.GetResourceStream("EXTRA13.HOG"));

            //Test 1:
            Assert.AreEqual("BLpyrobottomback1.ogf", hogFile.Lumps[0].Name);
            Assert.AreEqual(1940, hogFile.Lumps[0].Size);
            Assert.AreEqual(939679392, hogFile.Lumps[0].Timestamp);

            //Test 2: Black Pyro model
            Assert.AreEqual("purplepyro.OOF", hogFile.Lumps[21].Name);
            Assert.AreEqual(31941, hogFile.Lumps[21].Size);
            Assert.AreEqual(941690214, hogFile.Lumps[21].Timestamp);
        }

        [Test]
        public void TestHOG2LumpReader()
        {
            HOG2File hogFile = new HOG2File(TestUtils.GetResourceStream("EXTRA13.HOG"));

            //Test 1: OGF file, as byte array
            byte[] data = hogFile.GetLumpData(0);
            Assert.AreEqual(1940, data.Length);
            Assert.AreEqual(0, data[0]);
            Assert.AreEqual(0, data[1]);
            Assert.AreEqual(122, data[2]); //OUTRAGE_1555_COMPRESSED_MIPPED

            //Test 2: Black Pyro model, as stream
            BinaryReader br = new BinaryReader(hogFile.GetLumpAsStream(21));
            uint test = br.ReadUInt32();
            Assert.AreEqual(31941, br.BaseStream.Length);
            Assert.AreEqual(Util.MakeSig('P', 'S', 'P', 'O'), test);
        }
    }
}
