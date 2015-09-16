using System;

namespace libaxolotl
{
    public class InvalidKeyIdException : Exception
    {
        public InvalidKeyIdException(String detailMessage)
            : base(detailMessage)
        {
        }

        public InvalidKeyIdException(Exception exception)
            : base(exception.Message)
        {
        }
    }
}
