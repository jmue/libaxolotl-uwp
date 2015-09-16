using System;

namespace libaxolotl
{
    public class InvalidVersionException : Exception
    {
        public InvalidVersionException()
        {
        }

        public InvalidVersionException(String detailMessage)
            : base (detailMessage)
        {
        }
    }
}
