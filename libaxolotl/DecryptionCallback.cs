namespace libaxolotl
{
    public interface DecryptionCallback
    {
        void handlePlaintext(byte[] plaintext);
    }
}
