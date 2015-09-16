using System;

namespace libaxolotl
{
    public class DuplicateMessageException : Exception
    {
        public DuplicateMessageException(String s)
            : base(s)
        {
        }
    }
}
