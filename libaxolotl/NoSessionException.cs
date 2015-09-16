using System;

namespace libaxolotl
{
    public class NoSessionException : Exception
    {
        public NoSessionException()
        {
        }

        public NoSessionException(String s)
            : base(s)
        {
        }

        public NoSessionException(Exception exception)
           : base(exception.Message)
        {
        }
    }
}
