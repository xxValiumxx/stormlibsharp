using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;

namespace StormLibSharp.Unsafe.Interop
{
    internal unsafe delegate bool STREAM_GETPOS(TFileStream* pStream, ref ulong byteOffset);
    internal unsafe delegate bool STREAM_READ(TFileStream* pStream, ref ulong pByteOffset, void* pwBuffer, uint dwByteStoRead);
    internal unsafe delegate bool STREAM_WRITE(TFileStream* pStream, ref ulong pByteOffset, void* pvBuffer, uint dwBytesToWrite);
    internal unsafe delegate bool STREAM_GETSIZE(TFileStream* pStream, ref ulong FileSize);
    internal unsafe delegate bool STREAM_SETSIZE(TFileStream* pStream, ulong FileSize);

    internal enum StreamFlags : byte
    {
        None = 0, 
        ReadOnly = 1,
        PartFile = 2,
        EncryptedFile = 4,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct TFileStream
    {
        internal ulong RawFilePos;
        internal IntPtr hFile;
        internal fixed byte szFileName[260];
        internal StreamFlags StreamFlags;

        internal IntPtr StreamGetPos;
        internal IntPtr StreamRead;
        internal IntPtr StreamWrite;
        internal IntPtr StreamGetSize;
        internal IntPtr StreamSetSize;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct _PART_FILE_HEADER
    {
        internal uint PartialVersion;
        internal fixed byte GameBuildNumber[8];
        internal fixed uint Unknown[6];
        internal uint ZeroValue;
        internal uint FileSizeLo;
        internal uint FileSizeHi;
        internal uint PartSize;
    }

    internal struct _PART_FILE_MAP_ENTRY
    {
        internal uint Flags;
        internal uint BlockOffsLo;
        internal uint BlockOffsHi;
        internal uint Unknown0C;
        internal uint Unknown10;
    }

    internal unsafe struct TPartFileStream
    {
        // TFileStream
        internal ulong RawFilePos;
        internal IntPtr hFile;
        internal fixed byte szFileName[260];
        internal StreamFlags StreamFlags;

        internal IntPtr StreamGetPos;
        internal IntPtr StreamRead;
        internal IntPtr StreamWrite;
        internal IntPtr StreamGetSize;
        internal IntPtr StreamSetSize;
        

        internal ulong VirtualSize;
        internal ulong VirtualPos;
        internal uint PartCount;
        internal uint PartSize;

        //internal fixed _PART_FILE_MAP_ENTRY PartMap[1];
    }

    internal unsafe struct TEncryptedStream
    {
        // TFileStream
        internal ulong RawFilePos;
        internal IntPtr hFile;
        internal fixed byte szFileName[260];
        internal StreamFlags StreamFlags;

        internal IntPtr StreamGetPos;
        internal IntPtr StreamRead;
        internal IntPtr StreamWrite;
        internal IntPtr StreamGetSize;
        internal IntPtr StreamSetSize;
        

        internal fixed byte Key[0x40];
    }

    internal static class FileStreamManager
    {
        private enum Ops { GetPos, Read, Write, GetSize, SetSize };

        private static Dictionary<IntPtr, FileStream> _handlesToStreams = new Dictionary<IntPtr, FileStream>();
        private static Dictionary<Ops, IntPtr> _streamCallbacks = new Dictionary<Ops, IntPtr>()
        {
            { Ops.GetPos, Marshal.GetFunctionPointerForDelegate((STREAM_READ)GetPos) },
        };

        internal static unsafe TFileStream CreateTFileStream(string filePath, FileMode mode = FileMode.Open, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read)
        {
            FileStream fs = new FileStream(filePath, mode, access, share);
            SafeFileHandle handle = fs.SafeFileHandle;
            IntPtr ptr = handle.DangerousGetHandle();
            _handlesToStreams.Add(ptr, fs);

            TFileStream result = new TFileStream();
            result.RawFilePos = 0;
            result.hFile = ptr;
            result.StreamFlags = StreamFlags.None;
            result.StreamGetPos = Marshal.GetFunctionPointerForDelegate((STREAM_GETPOS)GetPos);
            result.StreamRead = Marshal.GetFunctionPointerForDelegate((STREAM_READ)Read);
            result.StreamWrite = Marshal.GetFunctionPointerForDelegate((STREAM_WRITE)Write);
            result.StreamGetSize = Marshal.GetFunctionPointerForDelegate((STREAM_GETSIZE)GetSize);
            result.StreamSetSize = Marshal.GetFunctionPointerForDelegate((STREAM_SETSIZE)SetSize);

            return result;
        }

        private static unsafe bool GetPos(TFileStream* pStream, ref ulong byteOffset)
        {
            if (pStream == null)
                return false;

            FileStream fs;
            if (!_handlesToStreams.TryGetValue(pStream->hFile, out fs))
                return false;

            long pos = fs.Position;
            byteOffset = (ulong)pos;

            return true;
        }

        private static unsafe bool Read(TFileStream* pStream, ref ulong byteOffset, void* pwBuffer, uint dwBytesToRead)
        {
            Debug.Assert(dwBytesToRead < int.MaxValue, "Unsupported case; must read < 2GiB worth of data per chunk.");

            if (pStream == null)
                return false;

            FileStream fs;
            if (!_handlesToStreams.TryGetValue(pStream->hFile, out fs))
                return false;

            byte[] tmpBuf = new byte[dwBytesToRead];
            if (byteOffset != 0)
            {
                long pos = unchecked((long)byteOffset);
                fs.Seek(pos, SeekOrigin.Begin);
            }

            fs.Read(tmpBuf, 0, unchecked((int)dwBytesToRead));
            IntPtr dest = new IntPtr(pwBuffer);
            Marshal.Copy(tmpBuf, 0, dest, unchecked((int)dwBytesToRead));
            byteOffset = (ulong)fs.Position;

            return true;
        }

        private static unsafe bool Write(TFileStream* pStream, ref ulong pByteOffset, void* pvBuffer, uint dwBytesToWrite)
        {
            // Not supported in v1
            return false;
        }

        private static unsafe bool GetSize(TFileStream* pStream, ref ulong FileSize)
        {
            if (pStream == null)
                return false;

            FileStream fs;
            if (!_handlesToStreams.TryGetValue(pStream->hFile, out fs))
                return false;

            FileSize = (ulong)fs.Length;
            return true;
        }

        private static unsafe bool SetSize(TFileStream* pStream, ulong FileSize)
        {
            // Not supported in v1
            return false;
        }
    }
}
