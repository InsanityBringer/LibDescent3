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
    /// <summary>
    /// Represents a gamefile, used by Outrage's editor to compile HOG files. 
    /// </summary>
    public class Gamefile
    {
        /// <summary>
        /// Gets or sets the filename of the game file.
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// Gets or sets the source directory the game file will be found in. 
        /// </summary>
        public string Directory { get; set; }
        /// <summary>
        /// Gets the combined path of the game file.
        /// </summary>
        public string Path
        {
            get
            {
                if (Directory == "") return Filename;
                return Directory + '\\' + Filename;
            }
        }
    }
}
