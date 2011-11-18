using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StormLibSharp.Unsafe.Interop
{
    internal unsafe struct TMpqArchive
    {
        internal TFileStream* pStream;
        
        internal ulong UserDataPos;
        internal ulong MpqPos;
        
        internal TMpqArchive* haPatch;
        internal TMpqArchive* haBase;
        internal fixed byte szPatchPrefix[32];
        internal ulong cchPathPrefix;

        internal TMpqUserData* pUserData;
        internal TMpqHeader* pHeader;
        internal TMpqHash* pHashTable;
        internal TMpqHetTable* pHetTable;
        internal TFileEntry* pFileTable;

        internal TMpqUserData UserData;
        internal fixed byte HeaderData[0xd0];

        internal uint dwHETBlockSize;
        internal uint dwBETBlockSize;
        internal uint dwFileTableSize;
        internal uint dwMaxFileCount;
        internal uint dwSectorSize;
        internal uint dwFileFlags1;
        internal uint dwFileFlags2;
        internal uint dwAttrFlags;
        internal uint dwFlags;
    }

    internal unsafe struct TFileEntry
    {
        internal ulong ByteOffset;
        internal ulong FileTime;
        internal ulong BetHash;
        internal uint dwHashIndex;
        internal uint dwHetIndex;
        internal uint dwFileSize;
        internal uint dwCmpSize;
        internal uint dWFlags;
        internal ushort lcLocale;
        internal ushort wPlatform;
        internal uint dwCrc32;
        internal fixed byte md5[16];
        internal byte* szFileName;
    }
}
