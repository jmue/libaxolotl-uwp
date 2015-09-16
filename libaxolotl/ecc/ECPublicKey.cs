using System;

namespace libaxolotl.ecc
{
    public interface ECPublicKey : IComparable
    {
        //int KEY_SIZE = 33;

        byte[] serialize();

        int getType();
    }
}
