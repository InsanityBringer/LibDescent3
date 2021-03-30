using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
