using Google.ProtocolBuffers;
using libaxolotl.ecc;
using static libaxolotl.state.StorageProtos;

namespace libaxolotl
{
    /**
     * Holder for public and private identity key pair.
     *
     * @author
     */
    public class IdentityKeyPair
    {

        private readonly IdentityKey publicKey;
        private readonly ECPrivateKey privateKey;

        public IdentityKeyPair(IdentityKey publicKey, ECPrivateKey privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }

        public IdentityKeyPair(byte[] serialized)
        {
            try
            {
                IdentityKeyPairStructure structure = IdentityKeyPairStructure.ParseFrom(serialized);
                this.publicKey = new IdentityKey(structure.PublicKey.ToByteArray(), 0);
                this.privateKey = Curve.decodePrivatePoint(structure.PrivateKey.ToByteArray());
            }
            catch (InvalidProtocolBufferException e)
            {
                throw new InvalidKeyException(e);
            }
        }

        public IdentityKey getPublicKey()
        {
            return publicKey;
        }

        public ECPrivateKey getPrivateKey()
        {
            return privateKey;
        }

        public byte[] serialize()
        {
            return IdentityKeyPairStructure.CreateBuilder()
                                           .SetPublicKey(ByteString.CopyFrom(publicKey.serialize()))
                                           .SetPrivateKey(ByteString.CopyFrom(privateKey.serialize()))
                                           .Build().ToByteArray();
        }
    }
}
