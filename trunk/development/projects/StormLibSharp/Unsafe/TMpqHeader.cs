using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace StormLibSharp.Unsafe
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TMpqHeader
    {
        // v1
        internal uint dwID;
        internal uint dwHeaderSize;
        internal uint dwArchiveSize;
        internal ushort wFormatVersion;
        internal ushort wSectorSize;
        internal uint dwHashTablePos;
        internal uint dwBlockTablePos;
        internal uint dwHashTableSize;
        internal uint dwBlockTableSize;

        // v2
        internal ulong HiBlockTablePos64;
        internal ushort wHashTablePosHi;
        internal ushort wBlockTablePosHi;

        // v3
        internal ulong ArchiveSize64;
        internal ulong BetTablePos64;
        internal ulong HetTablePos64;

        // v4
        internal ulong HashTableSize64;
        internal ulong BlockTableSize64;
        internal ulong HiBlockTableSize64;
        internal ulong HetTableSize64;
        internal ulong BetTableSize64;
        internal uint dwRawChunkSize;
        internal fixed byte MD5_BlockTable[16];
        internal fixed byte MD5_HashTable[16];
        internal fixed byte MD5_HiBlockTable[16];
        internal fixed byte MD5_BetTable[16];
        internal fixed byte MD5_HetTable[16];
        internal fixed byte MD5_MpqHeader[16];
    }
}
