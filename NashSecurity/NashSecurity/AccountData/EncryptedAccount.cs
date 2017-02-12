namespace NashSecurity.AccountData
{
    public class EncryptedAccount
    {
        public string UserName { get; set; }
        public byte[] EncryptedMasterPassword { get; set; }
        public byte[] EncryptedLoginPassword { get; set; }
    }
}