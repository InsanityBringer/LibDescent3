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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LibDescent3
{
    /// <summary>
    /// The format of the bitmap, as stored on disk.
    /// </summary>
    public enum BitmapType
    {
        //Standard TGA types
        TGANoImage = 0,
        TGAUncompressedColormapped = 1,
        TGAUncompressedTruecolor = 2,
        TGAUncompressedGrayscale = 3,
        TGACompressedColormapped = 9,
        TGACompressedTruecolor = 10,

        //Outrage image types
        //Not an actual value, but used to isolate the Outrage OGF types
        OutrageTypeStart = 120,
        Outrage4444CompressedMipped = 121,
        Outrage1555CompressedMipped = 122,
        OutrageNewCompressedMipped = 123,
        OutrageCompressedMipped = 124,
        //OutrageCompressedOGF8Bit = 125, //[ISB] appears support for this format was pulled.
        OutrageTGAType = 126,
        OutrageCompressedOGF = 127
    }
    public enum ImageType
    {
        Format4444,
        Format1555
    }
    /// <summary>
    /// Represents a Descent 3 bitmap image.
    /// </summary>
    public class Descent3Bitmap
    {
        /// <summary>
        /// Width of the image, in pixels.
        /// </summary>
        public ushort Width { get; }
        /// <summary>
        /// Height of the image, in pixels.
        /// </summary>
        public ushort Height { get; }
        /// <summary>
        /// Pixel size of the image's raw data.
        /// </summary>
        public byte RawPixelSize { get; }
        /// <summary>
        /// 16 BPP image data.
        /// </summary>
        public ushort[][] Data16BPP { get; }
        /// <summary>
        /// Number of mip levels for this image. 
        /// </summary>
        public int MipLevels { get; }
        /// <summary>
        /// Internal name of the bitmap file. Used by OGF files, but not TGAs.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Type of the bitmap, used for serialization. 
        /// </summary>
        public BitmapType Type { get; }
        public ImageType Format { get; }

        public Descent3Bitmap(BitmapType type, ImageType format, string name, int width, int height, int miplevels, int pixsize)
        {
            Width = (ushort)width;
            Height = (ushort)height;
            MipLevels = miplevels;
            RawPixelSize = (byte)pixsize;
            Type = type;
            Name = name;
            Format = format;

            //Allocate the data
            Data16BPP = new ushort[miplevels][];

            for (int i = 0; i < miplevels; i++)
            {
                Data16BPP[i] = new ushort[GetWidth(i) * GetHeight(i)];
            }
        }

        public int GetWidth(int mipLevel)
        {
            return Width / ((mipLevel + 1) * (mipLevel + 1));
        }

        public int GetHeight(int mipLevel)
        {
            return Height / ((mipLevel + 1) * (mipLevel + 1));
        }

        /// <summary>
        /// Reads a bitmap file from the specified stream.
        /// </summary>
        /// <param name="br">A binary reader wrapping the stream to read from.</param>
        /// <param name="preferredType">The preferred type of the image, for TGA files. Loaded TGA files will be converted to this format.</param>
        /// <returns>The bitmap processed from the stream.</returns>
        public static Descent3Bitmap ReadBitmapFromStream(BinaryReader br, ImageType preferredType = ImageType.Format4444)
        {
            byte imageIDLen = br.ReadByte();
            byte colorMapType = br.ReadByte();
            BitmapType type = (BitmapType)br.ReadByte();
            string name = "";
            int numMipLevels = 1;

            //todo: fix
            if (colorMapType != 0)
                throw new InvalidDataException("Cannot read TGAs with colormaps.");

            //if (type == BitmapType.OutrageCompressedOGF8Bit)
            //    data8Bit = true;


            if (type > BitmapType.OutrageTypeStart)
            {
                //Set the proper format on Outrage types
                if (type == BitmapType.Outrage4444CompressedMipped)
                    preferredType = ImageType.Format4444;
                else
                    preferredType = ImageType.Format1555;

                if (type == BitmapType.Outrage1555CompressedMipped || type == BitmapType.Outrage4444CompressedMipped || type == BitmapType.OutrageNewCompressedMipped)
                {
                    name = Util.ReadStringHelper(br, 34);
                }
                else
                {
                    char[] buf = br.ReadChars(35);
                    name = new string(buf);
                    //split out null
                    if (name.Contains('\0'))
                    {
                        name = name.Split('\0')[0];
                    }
                }

                if (type == BitmapType.Outrage1555CompressedMipped || type == BitmapType.Outrage4444CompressedMipped || type == BitmapType.OutrageNewCompressedMipped || type == BitmapType.OutrageCompressedMipped)
                {
                    numMipLevels = br.ReadByte();
                }
            }

            br.ReadBytes(9); //skip padding

            ushort width = br.ReadUInt16();
            ushort height = br.ReadUInt16();
            byte pixSize = br.ReadByte();

            if (pixSize != 24 && pixSize != 32)
                throw new InvalidDataException("TGAs can only be read with 24 or 32 bit color.");

            Descent3Bitmap bm = new Descent3Bitmap(type, preferredType, name, width, height, numMipLevels, pixSize);

            byte descriptor = br.ReadByte();
            br.ReadBytes(imageIDLen);

            bool upsideDown = 1 - ((descriptor & 0x20) >> 5) == 1;

            //carnival of image format reading
            int pixel;
            if (type == BitmapType.TGACompressedTruecolor)
            {
                int total = 0;
                int length;
                byte command;

                while (total < width * height)
                {
                    command = br.ReadByte();
                    length = (command & 127) + 1;

                    if ((command & 128) != 0)
                    {

                    }
                }
            }

            else if (type > BitmapType.OutrageTypeStart)
            {
                byte[] buf = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
                bm.InitializeFromCompressedOutrageData(buf);
            }

            return bm;
        }

        private void InitializeFromCompressedOutrageData(byte[] data)
        {
            int count, total;
            byte rleCount;
            ushort pixel;
            int offset = 0;
            int r, g, b, i;

            for (int level = 0; level < MipLevels; level++)
            {
                total = GetWidth(level) * GetHeight(level);
                count = 0;

                while (count < total)
                {
                    rleCount = data[offset++];
                    if (rleCount == 0) //read one pixel
                    {
                        pixel = (ushort)(data[offset] | (data[offset + 1] << 8));
                        offset += 2;

                        if (Type != BitmapType.Outrage1555CompressedMipped || Type != BitmapType.Outrage4444CompressedMipped)
                        {
                            if (pixel == 0x07e0)
                                pixel = ColorUtils.NewTransparentColor;
                            else
                            {
                                r = ((pixel & 0xF800) >> 11) << 3;
                                g = ((pixel & 0x07e0) >> 5) << 2;
                                b = (pixel & 0x001f) << 3;

                                pixel = (ushort)(ColorUtils.OpaqueFlag | (r << 10) | (g << 5) | b);
                            }

                            Data16BPP[level][count] = pixel;
                            count++;
                        }
                    }
                    else if (rleCount >= 2 && rleCount <= 250)
                    {
                        pixel = (ushort)(data[offset] | (data[offset + 1] << 8));
                        offset += 2;

                        if (Type != BitmapType.Outrage1555CompressedMipped || Type != BitmapType.Outrage4444CompressedMipped)
                        {
                            if (pixel == 0x07e0)
                                pixel = ColorUtils.NewTransparentColor;
                            else
                            {
                                r = ((pixel & 0xF800) >> 11) << 3;
                                g = ((pixel & 0x07e0) >> 5) << 2;
                                b = (pixel & 0x001f) << 3;

                                pixel = (ushort)(ColorUtils.OpaqueFlag | (r << 10) | (g << 5) | b);
                            }

                            for (i = 0; i < rleCount; i++)
                            {
                                Data16BPP[level][count] = pixel;
                                count++;
                            }
                        }
                    }
                }
            }
        }
    }
}
