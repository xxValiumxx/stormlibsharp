using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StormLibSharp
{
    public enum MpqLoadOptions
    {
        Default = 0,
        DoNotLoadListfile = 16,
        DoNotLoadAttributes = 32,
        ForceVersion1 = 64,
        CheckSectorRedundancy = 128,
        OpenReadOnly = 256,
        OpenEncrypted = 512,
    }
}
