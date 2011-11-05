using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace StormLibSharp.Unsafe
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct TMpqUserData
    {
        public uint dwID;
        public uint cbUserDataSize;
        public uint dwHeaderOffs;
        public uint cbUserDataHeader;
    }
}
