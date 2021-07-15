/*-----------------------------------------------------------------------------
 *  Copyright (c) 2021 SaladBadger
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *  The above copyright notice and this permission notice shall be included in all
 *  copies or substantial portions of the Software.
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *  SOFTWARE.
-----------------------------------------------------------------------------*/


namespace LibDescent3
{
    public static class ColorUtils
    {
        public const int NewTransparentColor = 0x0000;
        public const int OpaqueFlag = 0x8000;
        public static ushort Convert32BitToRGBA4444(uint color)
        {
            int r = (int)((color >> 20) & 15);
            int g = (int)((color >> 12) & 15);
            int b = (int)((color >> 4) & 15);
            int a = (int)((color >> 28) & 15);

            return (ushort)((r << 8) | (g << 4) | b | (a << 12));
        }

        public static ushort Convert32BitToRGBA5551(uint color)
        {
            int r = (int)((color >> 19) & 31);
            int g = (int)((color >> 11) & 31);
            int b = (int)((color >> 3) & 31);
            int a = (int)((color >> 27) & 31);

            if (a == 0) return NewTransparentColor;

            return (ushort)(OpaqueFlag | (r << 10) | (g << 5) | b);
        }

        //TODO: These can be made into LUTs. Would only take up half a meg at most, so memory shouldn't be a big problem. 
        public static int ConvertRGBA4444To32Bit(ushort color)
        {
            int r = (int)((color >> 8) & 15) * 255 / 15;
            int g = (int)((color >> 4) & 15) * 255 / 15;
            int b = (int)(color & 15) * 255 / 15;
            int a = (int)((color >> 12) & 15) * 255 / 15;

            return ((r << 16) | (g << 8) | b | (a << 24));
        }

        public static int ConvertRGBA5551To32Bit(ushort color)
        {
            int r = (int)((color >> 10) & 31) * 255 / 31;
            int g = (int)((color >> 5) & 31) * 255 / 31;
            int b = (int)(color & 31) * 255 / 31;
            int a = (int)((color >> 15) & 1) == 1 ? 255 : 0;

            return ((r << 16) | (g << 8) | b | (a << 24));
        }
    }
}
