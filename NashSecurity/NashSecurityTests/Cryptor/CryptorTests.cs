using System;
using NashSecurity.InternalCryptor;
using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.SecuritySystemTests;
using NUnit.Framework;

namespace NashSecurity.Tests.Cryptor
{
    public partial class CryptorTests : CryptorCreatedScenarioTests
    {
        public CryptorTests(Type enteredInSecurityContextType) : base(enteredInSecurityContextType)
        {
        }

        [Test]
        public void Encrypt()
        {
            byte[] inputBytes = new byte[] { 00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 1, 16, 17, 18, 19, 20 };
            byte[] expectedEncryptedBytes = InternalContentCryptor.EncryptBytes(AccountInfo.MasterPassword, inputBytes);
            var encryptedBytes = Cryptor.Encrypt(SessionToken, inputBytes);
            CollectionAssert.AreEqual(expectedEncryptedBytes, encryptedBytes);
        }

        [Test]
        public void Decrypt()
        {
            byte[] plainBytes = new byte[] { 00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 1, 16, 17, 18, 19, 20 };
            byte[] encryptedBytes = InternalContentCryptor.EncryptBytes(AccountInfo.MasterPassword, plainBytes);

            byte[] expectedDecryptedBytes = InternalContentCryptor.DecryptBytes(AccountInfo.MasterPassword, encryptedBytes);
            var decryptedBytes = Cryptor.Decrypt(SessionToken, encryptedBytes);
            CollectionAssert.AreEqual(expectedDecryptedBytes, decryptedBytes);
        }
    }
}
