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
    public struct WeaponFiringMask
    {
        public byte FireMask { get; set; }
        public float WaitTime { get; set; }
        public float AnimTime { get; set; }
        public float AnimStartFrame { get; set; }
        public float AnimFireFrame { get; set; }
        public float AnimEndFrame { get; set; }

        public bool FiresFromGunpoint1
        {
            get
            {
                return (FireMask & 1) != 0;
            }
            set
            {
                if (value)
                    FireMask |= 1;
                else
                    FireMask = (byte)(FireMask & ~1);
            }
        }
        public bool FiresFromGunpoint2
        {
            get
            {
                return (FireMask & 2) != 0;
            }
            set
            {
                if (value)
                    FireMask |= 2;
                else
                    FireMask = (byte)(FireMask & ~2);
            }
        }
        public bool FiresFromGunpoint3
        {
            get
            {
                return (FireMask & 4) != 0;
            }
            set
            {
                if (value)
                    FireMask |= 4;
                else
                    FireMask = (byte)(FireMask & ~4);
            }
        }
        public bool FiresFromGunpoint4
        {
            get
            {
                return (FireMask & 8) != 0;
            }
            set
            {
                if (value)
                    FireMask |= 8;
                else
                    FireMask = (byte)(FireMask & ~8);
            }
        }
        public bool FiresFromGunpoint5
        {
            get
            {
                return (FireMask & 16) != 0;
            }
            set
            {
                if (value)
                    FireMask |= 16;
                else
                    FireMask = (byte)(FireMask & ~16);
            }
        }
        public bool FiresFromGunpoint6
        {
            get
            {
                return (FireMask & 32) != 0;
            }
            set
            {
                if (value)
                    FireMask |= 32;
                else
                    FireMask = (byte)(FireMask & ~32);
            }
        }
        public bool FiresFromGunpoint7
        {
            get
            {
                return (FireMask & 64) != 0;
            }
            set
            {
                if (value)
                    FireMask |= 64;
                else
                    FireMask = (byte)(FireMask & ~64);
            }
        }
        public bool FiresFromGunpoint8
        {
            get
            {
                return (FireMask & 128) != 0;
            }
            set
            {
                if (value)
                    FireMask |= 128;
                else
                    FireMask = (byte)(FireMask & ~128);
            }
        }
    }
    public class WeaponBatteryChunk
    {
        public const int MaxGunpoints = 8;
        public float EnergyUse { get; set; }
        public float AmmoUse { get; set; }
        public short[] GunpointWeaponIndex { get; } = new short[MaxGunpoints];
        public WeaponFiringMask[] FiringMasks { get; } = new WeaponFiringMask[MaxGunpoints];
        public byte NumFiringMasks { get; set; }
        public short AimingGunpointIndex { get; set; }
        public byte AimingFlags { get; set; }
        public float AimingDot { get; set; }
        public float AimingDist { get; set; }
        public float AimingXZDot { get; set; }
        public short Flags { get; set; }
        public byte QuadFireMask { get; set; }

        public bool Spray
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
        public bool AnimLocal
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
        public bool AnimFull
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
        public bool RandomFiringOrder
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
        public bool Guided
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
        public bool UseCustomFov
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
        public bool OnOffFiring
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
        public bool CustomMaxDist
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
        public bool UserTimeout
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
        public bool FireFVec
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
                    Flags &= ~512;
            }
        }
        public bool AimFVec
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
                    Flags &= ~1024;
            }
        }
        public bool FireAtTarget
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
                    Flags &= ~2048;
            }
        }
    }
}
