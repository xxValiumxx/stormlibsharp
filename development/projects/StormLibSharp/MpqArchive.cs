using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StormLibSharp
{
    /// <summary>
    /// Represents an MPQ archive, or a collection of MPQ archives patching a main MPQ archive.
    /// </summary>
    public class MpqArchive : IDisposable
    {
        #region constants
        private const uint ID_MPQ = 0x1a51504du;
        private const uint ID_MPQ_USERDATA = 0x1b51504du;
        #endregion

        #region fields
        private Stream _strm;
        private int _searchPriority;
        private MpqLoadOptions _options;
        #endregion

        #region constructors
        public MpqArchive(
            Stream source,
            int defaultSearchPriority = 1,
            MpqLoadOptions options = MpqLoadOptions.OpenEncrypted | MpqLoadOptions.OpenReadOnly)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (!source.CanRead)
                throw new ArgumentException("Source stream must be readable.", "source");
            if (!source.CanSeek)
                throw new ArgumentException("Source stream must be seekable.", "source");
            
            if (!options.HasFlag(MpqLoadOptions.OpenReadOnly))
                throw new NotSupportedException("Writable MPQ archives are not yet supported.");

            _strm = source;
            _searchPriority = defaultSearchPriority;
            _options = options;

            Initialize();
        }

        public MpqArchive(
            string path,
            int defaultSearchPriority = 1,
            MpqLoadOptions options = MpqLoadOptions.OpenEncrypted | MpqLoadOptions.OpenReadOnly)
            : this(CreateFileStreamFromPathAndOptions(path, options), defaultSearchPriority, options)
        {

        }
        #endregion

        #region private methods
        private static Stream CreateFileStreamFromPathAndOptions(string path, MpqLoadOptions options)
        {
            if (options.HasFlag(MpqLoadOptions.OpenReadOnly))
            {
                return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            else
            {
                return new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            }
        }

        private void Initialize()
        {

        }
        #endregion

        #region api methods

        #endregion

        #region IDisposable
        ~MpqArchive()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_strm != null)
                {
                    _strm.Dispose();
                    _strm = null;
                }
            }
        }
        #endregion
    }
}
