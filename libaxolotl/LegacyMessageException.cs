using System;

namespace libaxolotl
{
    public class LegacyMessageException : Exception
    {
        public LegacyMessageException()
        {
        }

        public LegacyMessageException(String s)
            : base(s)
        {
        }
    }
}
