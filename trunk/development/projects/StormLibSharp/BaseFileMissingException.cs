using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StormLibSharp
{
    /// <summary>
    /// Indicates that the file being accessed is a patch archive, but
    /// the base archive is missing or unable to be resolved.
    /// </summary>
    /// <portingNode>Corresponds to symbol <c>ERROR_BASE_FILE_MISSING</c>
    /// with default value of 10004 (stormlib.h line 134)</portingNode>
    public class BaseFileMissingException : Exception
    {
        public BaseFileMissingException()
        {

        }

        public BaseFileMissingException(string message)
            : base(message)
        {

        }

        public BaseFileMissingException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected BaseFileMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
