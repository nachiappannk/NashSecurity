using System.Text;

namespace NashSecurity.InternalCryptor
{
    public class InternalPasswordCryptor : InternalCryptor
    {
        public InternalPasswordCryptor(string key) : base(key)
        {
            RandomizingStringForSalt = "NashSecurity";
            RandomizingStringForMain = "AliciaNash";
            Iteration = 12;
        }
    }


    public static class InternalPaswordCryptorExtentions
    {
        public static byte[] EncryptPassword(this InternalPasswordCryptor cryptor, string password)
        {
            var passBytes = Encoding.ASCII.GetBytes(password);
            return cryptor.Encrypt(passBytes);
        }

        public static string DecryptPassword(this InternalPasswordCryptor cryptor, byte[] encryptedPassword)
        {
            var bytes = cryptor.Decrypt(encryptedPassword);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}