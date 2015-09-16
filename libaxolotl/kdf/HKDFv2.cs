namespace libaxolotl.kdf
{
    public class HKDFv2 : HKDF
    {
        protected override int getIterationStartOffset()
        {
            return 0;
        }
    }
}
