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
    public struct PhysicsChunk
    {
        /// <summary>
        /// Gets or sets the mass of this object.
        /// </summary>
        public float Mass { get; set; }
        /// <summary>
        /// Gets or sets the drag factor of this object.
        /// </summary>
        public float Drag { get; set; }
        /// <summary>
        /// Gets or sets the maximum thrust of this object.
        /// </summary>
        public float MaxThrust { get; set; }
        /// <summary>
        /// Gets or sets the raw flags for this physics chunk.
        /// </summary>
        public uint Flags { get; set; }
        /// <summary>
        /// Gets or sets the drag factor of rotation of this object.
        /// </summary>
        public float RotDrag { get; set; }
        /// <summary>
        /// Gets or sets the maximum rotational thrust of this object.
        /// </summary>
        public float MaxRotThrust { get; set; }
        /// <summary>
        /// Gets or sets the maximum amount of bounces this object can have, if Bounce is set. If it is -1, it will bounce infinitely.
        /// </summary>
        public int NumBounces { get; set; }
        /// <summary>
        /// Gets or sets the initial velocity of this object, in units per second. Only used for weapons.
        /// </summary>
        public float InitialVelocity { get; set; }
        /// <summary>
        /// Gets or sets the initial rotational velocity of this object.
        /// </summary>
        public Vector InitialRotVel { get; set; }
        /// <summary>
        /// Gets or sets the amplitude of the object's wiggling, if Wiggle is set.
        /// </summary>
        public float WiggleAmplitude { get; set; }
        /// <summary>
        /// Gets or sets the period of the object's wiggling, if Wiggle is set.
        /// </summary>
        public float WigglesPerSec { get; set; }
        /// <summary>
        /// Gets or sets the multiplier applied to velocity when the object bounces.
        /// </summary>
        public float BounceCoefficient { get; set; }
        /// <summary>
        /// Gets or sets ???
        /// </summary>
        public float HitDieAngle { get; set; }
        /// <summary>
        /// Gets or sets how long it takes the object to reach the maximum turn roll, in seconds.
        /// </summary>
        public float TurnRollRate { get; set; }
        /// <summary>
        /// Gets or sets the amount of roll set at a given turning rate. 
        /// </summary>
        public float TurnRollRatio { get; set; }

        public bool TurnRoll
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
        public bool Leveling
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
        public bool Bounce
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
        public bool Wiggle
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
        public bool Sticks
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
        public bool Persistent
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
        public bool UsesThrust
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
        public bool Gravity
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
        public bool IgnoresSelfConcussionForce
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
        public bool Wind
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
        public bool UseParentVelocity
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
        public bool FixedVelocity
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
        public bool FixedRotVel
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
        public bool NoCollisionWithParent
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
        public bool CollidesWithSiblings
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
        public bool ReverseGravity
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
        public bool NoCollisions
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
        public bool NoCollisionsWithRobots
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
        public bool PointCollideWalls
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
        public bool Homing
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
        public bool Guided
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
        public bool IgnoreConcussiveForces
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
        public bool DestinationPosition
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
        public bool LockX
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
        public bool LockY
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
        public bool LockZ
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
        public bool LockP
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
        public bool LockH
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
        public bool LockB
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
        public bool NeverUseBigCollisionSphere
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
        public bool NoCollisionsWithSameType
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
        public bool NoCollisionsWithDoors
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
                    Flags &= ~0x80000000;
            }
        }
    }
}
