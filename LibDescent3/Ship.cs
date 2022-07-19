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
    public class ShipWeapon
    {
        public byte FiringFlags { get; set; }
        public string FireSound { get; set; }
        public string ReleaseSound { get; set; }
        public string DropPowerup { get; set; }
        public int MaxAmmo { get; set; }
        public WeaponBatteryChunk WeaponBattery { get; set; }
        public string[] FireSounds { get; } = new string[WeaponBatteryChunk.MaxGunpoints];
        public string[] WeaponName { get; } = new string[WeaponBatteryChunk.MaxGunpoints];
    }
    public class Ship
    {
        public const int NumPlayerWeapons = 21;
        public string Name { get; set; }
        public string CockpitFilename { get; set; }
        public string HUDConfigFilename { get; set; }
        public string Model { get; set; }
        public string DyingModel { get; set; }
        public string ModelMedDetail { get; set; }
        public string ModelLowDetail { get; set; }
        public float MedDetailDist { get; set; }
        public float LowDetailDist { get; set; }
        public PhysicsChunk PhysInfo { get; set; }
        public float Size { get; set; }
        public float DamageFactor { get; set; }
        public int Flags { get; set; }
        public ShipWeapon[] Weapons { get; } = new ShipWeapon[NumPlayerWeapons];
    }
}
