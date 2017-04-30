using NashSecurity.InternalCryptor;

namespace NashSecurity
{
    public class Cryptor : ICryptor
    {
        private readonly SessionDataHolder _sessionDataHolder;

        public Cryptor(SessionDataHolder sessionDataHolder)
        {
            _sessionDataHolder = sessionDataHolder;
        }

        public byte[] Encrypt(ISessionToken sessionToken, byte[] rawBytes)
        {
            string mainPassword = _sessionDataHolder.GetMasterPassword(sessionToken);
            return InternalContentCryptor.EncryptBytes(mainPassword, rawBytes);
        }

        public byte[] Decrypt(ISessionToken sessionToken, byte[] encryptedBytes)
        {
            string mainPassword = _sessionDataHolder.GetMasterPassword(sessionToken);
            return InternalContentCryptor.DecryptBytes(mainPassword, encryptedBytes);
        }
    }
}