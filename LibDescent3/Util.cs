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
    public static class Util
    {
        /// <summary>
        /// Makes a unsigned 32 bit integer representing a four character signature.
        /// Caveat/TODO: All chars must be <256 for this to work properly.
        /// </summary>
        /// <param name="a">The first character of the signature.</param>
        /// <param name="b">The second character of the signature.</param>
        /// <param name="c">The third character of the signature.</param>
        /// <param name="d">The fourth character of the signature.</param>
        /// <returns>A uint representing the signature.</returns>
        public static uint MakeSig(char a, char b, char c, char d)
        {
            return ((uint)d << 24) + ((uint)c << 16) + ((uint)b << 8) + a;
        }
    }
}
