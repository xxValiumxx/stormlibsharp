using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace StormLibSharp.Unsafe
{
    /// <summary>
    /// File description block contains information about the file.
    /// </summary>
    /// <portingNote>StormLib.h lines 579-594</portingNote>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct TMpqBlock
    {
        /// <summary>
        /// Offset to the beginning of the file, relative to the beginning of the
        /// archive.
        /// </summary>
        internal uint dwFilePos;
        /// <summary>
        /// Compressed file size.
        /// </summary>
        internal uint dwCSize;
        /// <summary>
        /// Uncompressed file size.  Only valid if the block is a file; otherwise
        /// meaningless and should be 0.
        /// </summary>
        internal uint dwFSize;
        /// <summary>
        /// Flags for the file.
        /// </summary>
        internal uint dwFlags;
    }
}
