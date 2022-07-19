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
        public static WeaponFiringMask ReadFiringMask(BinaryReader br)
        {
            WeaponFiringMask mask = new WeaponFiringMask();
            mask.FireMask = br.ReadByte();
            mask.WaitTime = br.ReadSingle();
            mask.AnimTime = br.ReadSingle();
            mask.AnimStartFrame = br.ReadSingle();
            mask.AnimFireFrame = br.ReadSingle();
            mask.AnimEndFrame = br.ReadSingle();
            return mask;
        }
        public static WeaponBatteryChunk ReadWeaponBattery(BinaryReader br)
        {
            WeaponBatteryChunk battery = new WeaponBatteryChunk();
            battery.EnergyUse = br.ReadSingle();
            battery.AmmoUse = br.ReadSingle();
            for (int i = 0; i < WeaponBatteryChunk.MaxGunpoints; i++)
                battery.GunpointWeaponIndex[i] = br.ReadInt16();
            for (int i = 0; i < WeaponBatteryChunk.MaxGunpoints; i++)
                battery.FiringMasks[i] = ReadFiringMask(br);
            battery.NumFiringMasks = br.ReadByte();
            battery.AimingGunpointIndex = br.ReadInt16();
            battery.AimingFlags = br.ReadByte();
            battery.AimingDot = br.ReadSingle();
            battery.AimingDist = br.ReadSingle();
            battery.AimingXZDot = br.ReadSingle();
            battery.Flags = br.ReadInt16();
            battery.QuadFireMask = br.ReadByte();
            return battery;
        }
        public static PhysicsChunk ReadPhysicsChunk(BinaryReader br)
        {
            PhysicsChunk chunk = new PhysicsChunk();
            chunk.Mass = br.ReadSingle();
            chunk.Drag = br.ReadSingle();
            chunk.MaxThrust = br.ReadSingle();
            chunk.Flags = br.ReadUInt32();
            chunk.RotDrag = br.ReadSingle();
            chunk.MaxRotThrust = br.ReadSingle();
            chunk.NumBounces = br.ReadInt32();
            chunk.InitialVelocity = br.ReadSingle();
            Vector vec = new Vector();
            vec.X = br.ReadSingle();
            vec.Y = br.ReadSingle();
            vec.Z = br.ReadSingle();
            chunk.InitialRotVel = vec;
            chunk.WiggleAmplitude = br.ReadSingle();
            chunk.WigglesPerSec = br.ReadSingle();
            chunk.BounceCoefficient = br.ReadSingle();
            chunk.HitDieAngle = br.ReadSingle();
            chunk.TurnRollRate = br.ReadSingle();
            chunk.TurnRollRatio = br.ReadSingle();
            return chunk;
        }
        public static Texture ReadTexture(BinaryReader br)
        {
            Texture texture = new Texture();
            short version = br.ReadInt16();
            texture.Name = Util.ReadStringHelper(br, 35);
            if (version > 7)
                throw new InvalidDataException(string.Format("Texture {1} specifies version {0}, but expected <= 7", version, texture.Name));
            texture.Bitmap = Util.ReadStringHelper(br, 35);
            texture.DestroyedBitmap = Util.ReadStringHelper(br, 35);

            texture.Red = br.ReadSingle();
            texture.Green = br.ReadSingle();
            texture.Blue = br.ReadSingle();
            texture.Alpha = br.ReadSingle();

            texture.Speed = br.ReadSingle();
            texture.SlideU = br.ReadSingle();
            texture.SlideV = br.ReadSingle();

            texture.Reflectivity = br.ReadSingle();

            texture.CoronaStyle = br.ReadByte();

            texture.Damage = br.ReadInt32();
            texture.Flags = br.ReadUInt32();

            if (texture.Procedural)
            {
                Procedural procedural = new Procedural();
                for (int i = 0; i < 255; i++)
                {
                    procedural.Palette[i] = br.ReadUInt16();
                }
                procedural.Heat = br.ReadByte();
                procedural.Light = br.ReadByte();
                procedural.Thickness = br.ReadByte();
                procedural.EvalTime = br.ReadSingle();
                procedural.OscillationTime = br.ReadSingle();
                procedural.OscillationValue = br.ReadByte();
                short numElements = br.ReadInt16();
                for (int i = 0; i < numElements; i++)
                {
                    ProceduralElement element = new ProceduralElement();
                    element.Type = br.ReadByte();
                    element.Freq = br.ReadByte();
                    element.Speed = br.ReadByte();
                    element.Size = br.ReadByte();
                    element.X1 = br.ReadByte();
                    element.Y1 = br.ReadByte();
                    element.X2 = br.ReadByte();
                    element.Y2 = br.ReadByte();
                    procedural.Elements.Add(element);
                }
                texture.ProceduralBlock = procedural;
            }

            texture.SoundName = Util.ReadStringHelper(br, 35);
            texture.SoundVolume = br.ReadSingle();

            return texture;
        }
        public static Door ReadDoor(BinaryReader br)
        {
            Door door = new Door();
            short version = br.ReadInt16();
            door.Name = Util.ReadStringHelper(br, 35);
            if (version > 3)
                throw new InvalidDataException(string.Format("Door {1} specifies version {0}, but expected 1, 2, or 3", version, door.Name));
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
            gamefile.Filename = Util.ReadStringHelper(br, 35);
            if (version > 2)
                throw new InvalidDataException(string.Format("Gamefile {1} specifies version {0}, but expected 1 or 2", version, gamefile.Filename));
            gamefile.Directory = Util.ReadStringHelper(br, 35);
            return gamefile;
        }

        public static Sound ReadSound(BinaryReader br)
        {
            Sound sound = new Sound();
            short version = br.ReadInt16();
            sound.Name = Util.ReadStringHelper(br, 35);
            if (version > 1)
                throw new InvalidDataException(string.Format("Sound {1} specifies version {0}, but expected 1", version, sound.Name));
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

        public static Ship ReadShip(BinaryReader br)
        {
            Ship ship = new Ship();
            short version = br.ReadInt16();
            ship.Name = Util.ReadStringHelper(br, 35);
            if (version > 6)
                throw new InvalidDataException(string.Format("Ship {1} specifies version {0}, but expected <= 6", version, ship.Name));

            ship.CockpitFilename = Util.ReadStringHelper(br, 35);
            ship.HUDConfigFilename = Util.ReadStringHelper(br, 35);
            ship.Model = Util.ReadStringHelper(br, 35);
            ship.DyingModel = Util.ReadStringHelper(br, 35);
            ship.ModelMedDetail = Util.ReadStringHelper(br, 35);
            ship.ModelLowDetail = Util.ReadStringHelper(br, 35);
            ship.MedDetailDist = br.ReadSingle();
            ship.LowDetailDist = br.ReadSingle();
            ship.PhysInfo = ReadPhysicsChunk(br);
            ship.Size = br.ReadSingle();
            ship.DamageFactor = br.ReadSingle();
            ship.Flags = br.ReadInt32();
            for (int i = 0; i < Ship.NumPlayerWeapons; i++)
            {
                ShipWeapon weapon = new ShipWeapon();
                weapon.FiringFlags = br.ReadByte();
                weapon.FireSound = Util.ReadStringHelper(br, 35);
                weapon.ReleaseSound = Util.ReadStringHelper(br, 35);
                weapon.DropPowerup = Util.ReadStringHelper(br, 35);
                weapon.MaxAmmo = br.ReadInt32();
                weapon.WeaponBattery = ReadWeaponBattery(br);
                for (int j = 0; j < WeaponBatteryChunk.MaxGunpoints; j++)
                    weapon.FireSounds[j] = Util.ReadStringHelper(br, 35);
                for (int j = 0; j < WeaponBatteryChunk.MaxGunpoints; j++)
                    weapon.WeaponName[j] = Util.ReadStringHelper(br, 35);

                ship.Weapons[i] = weapon;
            }
            return ship;
        }
    }
}
