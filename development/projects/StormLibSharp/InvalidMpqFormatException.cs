using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StormLibSharp
{
    [Serializable]
    public class InvalidMpqFormatException : Exception
    {
        public InvalidMpqFormatException()
        {

        }

        public InvalidMpqFormatException(string message)
            : base(message)
        {

        }

        public InvalidMpqFormatException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected InvalidMpqFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
