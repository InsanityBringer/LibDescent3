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

namespace LibDescent3
{
    public class Sound
    {
        /// <summary>
        /// Gets or sets the name of this sound.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the filename of the WAV file this sound plays.
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// Gets or sets the raw flags of this sound.
        /// </summary>
        public int Flags { get; set; }
        /// <summary>
        /// Gets or sets the number of the sample that loops will return to.
        /// </summary>
        public int LoopStart { get; set; }
        /// <summary>
        /// Gets or sets the number of the sample that the sound will loop when reached.
        /// </summary>
        public int LoopEnd { get; set; }
        /// <summary>
        /// Gets or sets the multiplier of the volume when the listener is outside the inner cone, but within the outer cone.
        /// </summary>
        public float OuterConeVolume { get; set; }
        /// <summary>
        /// Gets or sets the angle of the inner cone, in degrees. In this cone, the sound will play at full volume.
        /// </summary>
        public int InnerConeAngle { get; set; }
        /// <summary>
        /// Gets or sets the angle of the outer cone, in degrees. In this cone, the sound will play at the volume specified by OuterConeVolume.
        /// </summary>
        public int OuterConeAngle { get; set; }
        /// <summary>
        /// Gets or sets the distance that this sound will fade to zero volume.
        /// </summary>
        public float MaxDistance { get; set; }
        /// <summary>
        /// Gets or sets the distance that when closer, the sound will not get any louder.
        /// </summary>
        public float MinDistance { get; set; }
        /// <summary>
        /// Gets or sets the volume multiplier applied to the raw WAV file when played. 
        /// </summary>
        public float ImportVolumeAdjust { get; set; }

        public bool Looped
        {
            get
            {
                return (Flags & 1) != 0;
            }
            set
            {
                if (value)
                    Flags |= 1;
                else
                    Flags &= ~1;
            }
        } 
        public bool FixedFrequency
        {
            get
            {
                return (Flags & 2) != 0;
            }
            set
            {
                if (value)
                    Flags |= 2;
                else
                    Flags &= ~2;
            }
        } 
        public bool ObjUpdate
        {
            get
            {
                return (Flags & 4) != 0;
            }
            set
            {
                if (value)
                    Flags |= 4;
                else
                    Flags &= ~4;
            }
        }
        public bool PlaysForever
        {
            get
            {
                return (Flags & 8) != 0;
            }
            set
            {
                if (value)
                    Flags |= 8;
                else
                    Flags &= ~8;
            }
        }
        public bool PlaysExclusively
        {
            get
            {
                return (Flags & 16) != 0;
            }
            set
            {
                if (value)
                    Flags |= 16;
                else
                    Flags &= ~16;
            }
        }
        public bool PlaysOnce
        {
            get
            {
                return (Flags & 32) != 0;
            }
            set
            {
                if (value)
                    Flags |= 32;
                else
                    Flags &= ~32;
            }
        }
        public bool UseCone
        {
            get
            {
                return (Flags & 64) != 0;
            }
            set
            {
                if (value)
                    Flags |= 64;
                else
                    Flags &= ~64;
            }
        }
        public bool ListenerUpdate
        {
            get
            {
                return (Flags & 128) != 0;
            }
            set
            {
                if (value)
                    Flags |= 128;
                else
                    Flags &= ~128;
            }
        }
        public bool OncePerObj
        {
            get
            {
                return (Flags & 256) != 0;
            }
            set
            {
                if (value)
                    Flags |= 256;
                else
                    Flags &= ~256;
            }
        }
    }
}
