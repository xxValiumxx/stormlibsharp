using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StormLibSharp
{
    /// <summary>
    /// Represents the signature verification status of an MPQ archive.
    /// </summary>
    /// <portingNotes>From StormLib.h.  Unverified does not have a corresponding member
    /// in the original source.  Lines 320-325.</portingNotes>
    public enum SignatureVerification
    {
        /// <summary>
        /// Indicates that verification has not yet been attempted.
        /// </summary>
        Unverified = -1,
        /// <summary>
        /// Indicates that no signature was present in the archive.
        /// </summary>
        NoSignature = 0,
        /// <summary>
        /// Indicates that an internal error prevented verification, such as an out of 
        /// memory condition.
        /// </summary>
        VerificationFailed = 1,
        /// <summary>
        /// Indicates that the archive had a weak signature, and it was verified.
        /// </summary>
        WeakSignatureVerified = 2,
        /// <summary>
        /// Indicates that the archive had a weak signature, but was unable to be
        /// verified.
        /// </summary>
        WeakSignatureFailed = 3,
        /// <summary>
        /// Indicates that the archive had a strong signature, and it was verified.
        /// </summary>
        StrongSignatureVerified = 4,
        /// <summary>
        /// Indicates that the archive had a strong signature, but was unable to be 
        /// verified.
        /// </summary>
        StrongSignatureFailed = 5
    }
}
