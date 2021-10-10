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
    public class PageIO
    {
        public static Door ReadDoor(BinaryReader br)
        {
            Door door = new Door();
            short version = br.ReadInt16();
            if (version > 3)
                throw new InvalidDataException(string.Format("Door specifies version {0}, but expected 1, 2, or 3", version));
            door.Name = Util.ReadStringHelper(br, 35);
            door.ModelName = Util.ReadStringHelper(br, 35);
            door.OpeningTime = br.ReadSingle();
            door.CloseTime = br.ReadSingle();
            door.OpenTime = br.ReadSingle();
            door.Flags = br.ReadByte();
            if (version >= 3)
                door.Health = br.ReadInt16();

            door.OpenSound = Util.ReadStringHelper(br, 35);
            door.CloseSound = Util.ReadStringHelper(br, 35);

            if (version >= 2)
                door.ModuleName = Util.ReadStringHelper(br, 35);

            return door;
        }

        public static Gamefile ReadGamefile(BinaryReader br)
        {
            Gamefile gamefile = new Gamefile();
            short version = br.ReadInt16();
            if (version > 2)
                throw new InvalidDataException(string.Format("Gamefile specifies version {0}, but expected 1 or 2", version));
            gamefile.Filename = Util.ReadStringHelper(br, 35);
            gamefile.Directory = Util.ReadStringHelper(br, 35);
            return gamefile;
        }

        public static Sound ReadSound(BinaryReader br)
        {
            Sound sound = new Sound();
            short version = br.ReadInt16();
            if (version > 1)
                throw new InvalidDataException(string.Format("Sound specifies version {0}, but expected 1", version));
            sound.Name = Util.ReadStringHelper(br, 35);
            sound.Filename = Util.ReadStringHelper(br, 35);
            sound.Flags = br.ReadInt32();
            sound.LoopStart = br.ReadInt32();
            sound.LoopEnd = br.ReadInt32();
            sound.OuterConeVolume = br.ReadSingle();
            sound.InnerConeAngle = br.ReadInt32();
            sound.OuterConeAngle = br.ReadInt32();
            sound.MaxDistance = br.ReadSingle();
            sound.MinDistance = br.ReadSingle();
            sound.ImportVolumeAdjust = br.ReadSingle();
            return sound;
        }
    }
}
