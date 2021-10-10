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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibDescent3
{
    public class TableFile : IDataFile
    {
        public List<Door> Doors { get; } = new List<Door>();
        public List<Sound> Sounds { get; } = new List<Sound>();
        public List<Gamefile> Gamefiles { get; } = new List<Gamefile>();

        public void Read(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            long lastOffset;

            while (stream.Position < stream.Length)
            {
                byte type = br.ReadByte();
                lastOffset = stream.Position;
                int chunkLen = br.ReadInt32();

                switch (type)
                {
                    case 5:
                        Doors.Add(PageIO.ReadDoor(br));
                        break;
                    case 7:
                        Sounds.Add(PageIO.ReadSound(br));
                        break;
                    case 9:
                        Gamefiles.Add(PageIO.ReadGamefile(br));
                        break;
                    case 1:
                    case 2:
                    case 6:
                    case 8:
                    case 10:
                    case 0:
                        stream.Seek(chunkLen-4, SeekOrigin.Current);
                        break;
                    default:
                        throw new InvalidDataException(string.Format("Chunk at offset {0} specified unknown type {1}", lastOffset - 1, type));
                }

                //Diagnostics
                if (stream.Position - lastOffset != chunkLen)
                    throw new InvalidDataException(string.Format("Chunk type {3} at offset {0} specified {1} bytes, but read {2}", lastOffset - 1, chunkLen, stream.Position - lastOffset, type));
            }
        }

        public void Write(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
