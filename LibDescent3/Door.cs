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
    public class Door
    {
        /// <summary>
        /// Gets or sets the name of this door.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the model filename of this door.
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// Gets or sets the amount of time it takes for this door to open.
        /// </summary>
        public float OpeningTime { get; set; }
        /// <summary>
        /// Gets or sets the amount of time it takes for this door to close.
        /// </summary>
        public float CloseTime { get; set; }
        /// <summary>
        /// Gets or sets the amount of time this door will remain open.
        /// </summary>
        public float OpenTime { get; set; }
        /// <summary>
        /// Gets or sets the raw flags set by this door. Use Blastable or SeeThrough to get or set individual flags.
        /// </summary>
        public byte Flags { get; set; }
        /// <summary>
        /// Gets or sets the amount of health this door has, if it is blastable.
        /// </summary>
        public short Health { get; set; }
        /// <summary>
        /// Gets or sets the name of the sound played when the door opens.
        /// </summary>
        public string OpenSound { get; set; }
        /// <summary>
        /// Gets or sets the name of the sound played when the door closes.
        /// </summary>
        public string CloseSound { get; set; }
        /// <summary>
        /// Gets or sets the filename of the module used to control this door. If not specified, this defaults to "generic.dll".
        /// </summary>
        public string ModuleName { get; set; }

        public bool Blastable
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
                    Flags = (byte)(Flags & ~1);
            }
        }
        public bool SeeThrough
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
                    Flags = (byte)(Flags & ~2);
            }
        }
    }
}
