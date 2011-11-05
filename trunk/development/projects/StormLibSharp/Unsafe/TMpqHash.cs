using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace StormLibSharp.Unsafe
{
    /// <summary>
    /// Represents a hash entry.  All files in the archive are searched by
    /// their hashes.
    /// </summary>
    /// <portingNote>From StormLib.h, lines 542-576</portingNote>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct TMpqHash
    {
        internal uint dwName1;
        internal uint dwName2;
        internal ushort lcLocale;
        internal ushort wPlatform;
        /// <summary>
        /// If the hash table entry is valid, this is the index into
        /// the block table of the file.  Otherwise, one of the following
        /// two values:
        /// 0xffffffff: Hash table entry is empty and has always been 
        /// empty; terminates search for a given file.
        /// 0xfffffffe: Hash table entry is empty, but was valid at
        /// some point (a deleted file).  Does not terminate search
        /// for a given file.
        /// </summary>
        internal uint dwBlockIndex;
    }
}
