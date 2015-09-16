namespace libaxolotl.ecc
{
    public interface ECPrivateKey
    {
        byte[] serialize();
        int getType();
    }
}
