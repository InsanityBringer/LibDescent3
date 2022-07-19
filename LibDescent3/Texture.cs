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
    public enum ProceduralType
    {
        None,
        LineLightning,
        SphereLightning,
        Straight,
        RisingEmbers,
        RandomEmbers,
        Spinners,
        Roamers,
        Fountain,
        Cone,
        FallRight,
        FallLeft
    }
    public enum WaterProceduralType
    {
        None,
        HeightBlob,
        SineBlob,
        RandomRaindrops,
        RandomBlobdrops
    }
    public struct ProceduralElement
    {
        public byte Type { get; set; }
        public byte Freq { get; set; }
        public byte Speed { get; set; }
        public byte Size { get; set; }
        public byte X1 { get; set; }
        public byte Y1 { get; set; }
        public byte X2 { get; set; }
        public byte Y2 { get; set; }

        //This is dumb. The procedural types available are only controlled by the base texture's flags. 
        public ProceduralType ProcType { get { return (ProceduralType)Type; } }
        public WaterProceduralType WaterProcType { get { return (WaterProceduralType)Type; } }
    }
    public class Procedural
    {
        public ushort[] Palette { get; } = new ushort[256];
        public byte Heat { get; set; }
        public byte Light { get; set; }
        public byte Thickness { get; set; }
        public float EvalTime { get; set; }
        public float OscillationTime { get; set; }
        public byte OscillationValue { get; set; }
        public List<ProceduralElement> Elements { get; } = new List<ProceduralElement>();
        
    }
    public class Texture
    {
        public string Name { get; set; }
        public string Bitmap { get; set; }
        public string DestroyedBitmap { get; set; }
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }
        public float Alpha { get; set; }
        public float Speed { get; set; }
        public float SlideU { get; set; }
        public float SlideV { get; set; }
        public float Reflectivity { get; set; }
        public byte CoronaStyle { get; set; }
        public int Damage { get; set; }
        public uint Flags { get; set; }
        public Procedural ProceduralBlock { get; set; }
        public string SoundName { get; set; }
        public float SoundVolume { get; set; }

        public bool Volatile
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
                    Flags &= ~1U;
            }
        }
        public bool Water
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
                    Flags &= ~2U;
            }
        }
        public bool Metal
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
                    Flags &= ~4U;
            }
        }
        public bool Marble
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
                    Flags &= ~8U;
            }
        }
        public bool Plastic
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
                    Flags &= ~16U;
            }
        }
        public bool ForceField
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
                    Flags &= ~32U;
            }
        }
        public bool Animated
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
                    Flags &= ~64U;
            }
        }
        public bool Destroyable
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
                    Flags &= ~128U;
            }
        }
        public bool EffectTexture
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
                    Flags &= ~256U;
            }
        }
        public bool HudCockpitTexture
        {
            get
            {
                return (Flags & 512) != 0;
            }
            set
            {
                if (value)
                    Flags |= 512;
                else
                    Flags &= ~512U;
            }
        }
        public bool MineTexture
        {
            get
            {
                return (Flags & 1024) != 0;
            }
            set
            {
                if (value)
                    Flags |= 1024;
                else
                    Flags &= ~1024U;
            }
        }
        public bool TerrainTexture
        {
            get
            {
                return (Flags & 2048) != 0;
            }
            set
            {
                if (value)
                    Flags |= 2048;
                else
                    Flags &= ~2048U;
            }
        }
        public bool ObjectTexture
        {
            get
            {
                return (Flags & 4096) != 0;
            }
            set
            {
                if (value)
                    Flags |= 4096;
                else
                    Flags &= ~4096U;
            }
        }
        public bool Texture64
        {
            get
            {
                return (Flags & 8192) != 0;
            }
            set
            {
                if (value)
                    Flags |= 8192;
                else
                    Flags &= ~8192U;
            }
        }
        public bool OnTMap2
        {
            get
            {
                return (Flags & 16384) != 0;
            }
            set
            {
                if (value)
                    Flags |= 16384;
                else
                    Flags &= ~16384U;
            }
        }
        public bool Texture32
        {
            get
            {
                return (Flags & 32768) != 0;
            }
            set
            {
                if (value)
                    Flags |= 32768;
                else
                    Flags &= ~32768U;
            }
        }
        public bool FlyThrough
        {
            get
            {
                return (Flags & 65536) != 0;
            }
            set
            {
                if (value)
                    Flags |= 65536;
                else
                    Flags &= ~65536U;
            }
        }
        public bool PassThrough
        {
            get
            {
                return (Flags & 0x20000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x20000;
                else
                    Flags &= ~0x20000U;
            }
        }
        public bool PingPong
        {
            get
            {
                return (Flags & 0x40000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x40000;
                else
                    Flags &= ~0x40000U;
            }
        }
        public bool Light
        {
            get
            {
                return (Flags & 0x80000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x80000;
                else
                    Flags &= ~0x80000U;
            }
        }
        public bool Breakable
        {
            get
            {
                return (Flags & 0x100000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x100000;
                else
                    Flags &= ~0x100000U;
            }
        }
        public bool Saturate
        {
            get
            {
                return (Flags & 0x200000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x200000;
                else
                    Flags &= ~0x200000U;
            }
        }
        public bool UsesAlpha
        {
            get
            {
                return (Flags & 0x400000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x400000;
                else
                    Flags &= ~0x400000U;
            }
        }
        public bool DontUse
        {
            get
            {
                return (Flags & 0x800000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x800000;
                else
                    Flags &= ~0x800000U;
            }
        }
        public bool Procedural
        {
            get
            {
                return (Flags & 0x1000000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x1000000;
                else
                    Flags &= ~0x1000000U;
            }
        }
        public bool WaterProcedural
        {
            get
            {
                return (Flags & 0x2000000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x2000000;
                else
                    Flags &= ~0x2000000U;
            }
        }
        public bool ForceLightmap
        {
            get
            {
                return (Flags & 0x4000000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x4000000;
                else
                    Flags &= ~0x4000000U;
            }
        }
        public bool SaturateLightmap
        {
            get
            {
                return (Flags & 0x8000000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x8000000;
                else
                    Flags &= ~0x8000000U;
            }
        }
        public bool Texture256
        {
            get
            {
                return (Flags & 0x10000000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x10000000;
                else
                    Flags &= ~0x10000000U;
            }
        }
        public bool Lava
        {
            get
            {
                return (Flags & 0x20000000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x20000000;
                else
                    Flags &= ~0x20000000U;
            }
        }
        public bool Rubble
        {
            get
            {
                return (Flags & 0x40000000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x40000000;
                else
                    Flags &= ~0x40000000U;
            }
        }
        public bool SmoothSpecular
        {
            get
            {
                return (Flags & 0x80000000) != 0;
            }
            set
            {
                if (value)
                    Flags |= 0x80000000;
                else
                    Flags &= ~0x80000000U;
            }
        }
    }
}
