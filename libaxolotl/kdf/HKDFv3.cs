namespace libaxolotl.kdf
{
    public class HKDFv3 : HKDF
    {
        protected override int getIterationStartOffset()
        {
            return 1;
        }
    }
}
