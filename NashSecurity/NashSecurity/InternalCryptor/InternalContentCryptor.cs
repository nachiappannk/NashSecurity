namespace NashSecurity.InternalCryptor
{
    public class InternalContentCryptor : InternalCryptor
    {
        private InternalContentCryptor(string key) : base(key)
        {
            RandomizingStringForSalt = "NashSecurity";
            RandomizingStringForMain = "JohnFNash";
            Iteration = 11;
        }

        public static byte[] EncryptBytes(string key, byte[] inputBytes)
        {
            var cryptor = new InternalContentCryptor(key);
            return cryptor.Encrypt(inputBytes);
        }

        public static byte[] DecryptBytes(string key, byte[] inputBytes)
        {
            var cryptor = new InternalContentCryptor(key);
            return cryptor.Decrypt(inputBytes);
        }
    }
}