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
    public class HOG2Lump
    {
        /// <summary>
        /// The file name associated with this lump.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The flags of this lump. Appears to be unused.
        /// </summary>
        public uint Flags { get; set; }
        /// <summary>
        /// The size of the data of this lump.
        /// </summary>
        public uint Size { get; set; }
        /// <summary>
        /// The timestamp of when the lump was created, in unix time. May be 0.
        /// </summary>
        public uint Timestamp { get; set; }
        /// <summary>
        /// The offset of this lump within the .HOG that it is contained in.
        /// </summary>
        public uint Offset;

        /// <summary>
        /// The raw data contained within this lump. May be null if data hasn't been read.
        /// </summary>
        public byte[] Data; //Needed for imported items
    }
    public class HOG2File : IDataFile
    {
        /// <summary>
        /// Persistent stream to the HOG file, to allow loading lump data on demand.
        /// </summary>
        private BinaryReader fileStream;

        public List<HOG2Lump> Lumps { get; } = new List<HOG2Lump>();
        public int NumLumps { get => Lumps.Count; }

        private uint fileDataOffset;

        public HOG2File(Stream stream)
        {
            Read(stream);
        }

        public void Read(Stream stream)
        {
            if (!stream.CanSeek)
                throw new ArgumentException("HOG2File:Read: Passed stream must be seekable.");

            BinaryReader br = new BinaryReader(stream, Encoding.ASCII);
            fileStream = br;
            Lumps.Clear();

            uint header = br.ReadUInt32();
            if (header != Util.MakeSig('H', 'O', 'G', '2'))
            {
                throw new InvalidDataException("HOG2 file has bad header. Expected \"HOG2\".");
            }
            int numLumps = br.ReadInt32();
            fileDataOffset = br.ReadUInt32();

            br.BaseStream.Seek(68, SeekOrigin.Begin); //Skip header padding
            uint lastOffset = fileDataOffset;
            for (int i = 0; i < numLumps; i++)
            {
                char[] buf = br.ReadChars(36);
                HOG2Lump lump = new HOG2Lump();
                lump.Name = new string(buf);
                //split out null
                if (lump.Name.Contains('\0'))
                {
                    lump.Name = lump.Name.Split('\0')[0];
                }
                lump.Flags = br.ReadUInt32();
                lump.Size = br.ReadUInt32();
                lump.Timestamp = br.ReadUInt32();
                lump.Offset = lastOffset;
                lastOffset += lump.Size;
                Lumps.Add(lump);
            }
        }

        public void Write(Stream stream)
        {
            throw new NotImplementedException();
        }

        public byte[] GetLumpData(int lumpnum)
        {
            HOG2Lump lump = Lumps[lumpnum];
            fileStream.BaseStream.Seek(lump.Offset, SeekOrigin.Begin);
            return fileStream.ReadBytes((int)lump.Size); //I really hope you don't have any files >2gb in your HOG file..
        }

        public Stream GetLumpAsStream(int lumpnum)
        {
            byte[] data = GetLumpData(lumpnum);
            return new MemoryStream(data);
        }
    }
}
