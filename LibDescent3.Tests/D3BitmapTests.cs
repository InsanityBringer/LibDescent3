using System.IO;
using NUnit.Framework;

namespace LibDescent3.Tests
{
    class D3BitmapTests
    {
        [Test]
        public void TestReadBitmapHeader()
        {
            //Test 1: Unmipped image from HOG2 file.
            HOG2File hogFile = new HOG2File(TestUtils.GetResourceStream("EXTRA13.HOG"));
            BinaryReader br = new BinaryReader(hogFile.GetLumpAsStream(0));
            Descent3Bitmap bm = Descent3Bitmap.ReadBitmapFromStream(br);

            Assert.AreEqual(32, bm.Width);
            Assert.AreEqual(32, bm.Height);
            Assert.AreEqual(1, bm.MipLevels);
            Assert.AreEqual(BitmapType.Outrage1555CompressedMipped, bm.Type);
            Assert.AreEqual(ImageType.Format1555, bm.Format);
            Assert.AreEqual("BLpyrobottomback1.ogf", bm.Name);

            //Test 2: Mipped image from disk
            br.Close();

            br = new BinaryReader(TestUtils.GetResourceStream("Angle Tile 1.ogf"));
            bm = Descent3Bitmap.ReadBitmapFromStream(br);

            Assert.AreEqual(128, bm.Width);
            Assert.AreEqual(128, bm.Height);
            Assert.AreEqual(5, bm.MipLevels);
            Assert.AreEqual(BitmapType.Outrage1555CompressedMipped, bm.Type);
            Assert.AreEqual("Angle Tile 1.ogf", bm.Name);
            Assert.AreEqual(ImageType.Format1555, bm.Format);

            //Test 3: unmipped 4444 image from disk
            br.Close();

            br = new BinaryReader(TestUtils.GetResourceStream("CED Grid2S.ogf"));
            bm = Descent3Bitmap.ReadBitmapFromStream(br);

            Assert.AreEqual(128, bm.Width);
            Assert.AreEqual(128, bm.Height);
            Assert.AreEqual(1, bm.MipLevels);
            Assert.AreEqual(BitmapType.Outrage4444CompressedMipped, bm.Type);
            Assert.AreEqual("CED Grid2S.ogf", bm.Name);
            Assert.AreEqual(ImageType.Format4444, bm.Format);
        }
    }
}
