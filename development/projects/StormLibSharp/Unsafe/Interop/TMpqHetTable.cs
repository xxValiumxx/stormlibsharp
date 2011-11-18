using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StormLibSharp.Unsafe.Interop
{
    internal unsafe struct TMpqHetTable
    {
        internal byte* pBetIndexes;
        internal byte* pHetHashes;
        internal ulong AndMask64;
        internal ulong OrMask64;
        internal uint dwIndexSizeTotal;
        internal uint dwIndexSizeExtra;
        internal uint dwMaxFileCount;
        internal uint dwHashTableSize;
        internal uint dwHashBitSize;
    }
}
