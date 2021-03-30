using NUnit.Framework;

namespace LibDescent3.Tests
{
    class ColorConversionTests
    {
        [Test]
        public void TestColorConversion()
        {
            Assert.AreEqual(0xFFFF, ColorUtils.Convert32BitToRGBA4444(0xFFFFFFFF));
            Assert.AreEqual(0xF777, ColorUtils.Convert32BitToRGBA4444(0xFF7F7F7F));
            Assert.AreEqual(0x8000, ColorUtils.Convert32BitToRGBA4444(0x80000000));
        }
    }
}
