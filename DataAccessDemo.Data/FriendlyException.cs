using System;
using System.Runtime.Serialization;

namespace DataAccessDemo.Data
{
    [Serializable]
    public class FriendlyException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public FriendlyException()
        {
        }

        public FriendlyException(string message) : base(message)
        {
        }

        public FriendlyException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FriendlyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
