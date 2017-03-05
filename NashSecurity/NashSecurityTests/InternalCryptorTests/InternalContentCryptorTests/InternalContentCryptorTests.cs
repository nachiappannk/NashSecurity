﻿using NashSecurity.InternalCryptor;
using NUnit.Framework;

namespace NashSecurity.Tests.InternalCryptorTests.InternalContentCryptorTests
{
    [TestFixture]
    public partial class InternalContentCryptorTests
    {
        private static void AssertEncryptedBytesAreCorrect(byte[] expectedEncryptedBytes, string encryptionKey, 
            byte[] inputBytes)
        {
             var bytes = InternalContentCryptor.EncryptBytes(encryptionKey,inputBytes);
            //Debug.Print(string.Join(",",bytes));
            CollectionAssert.AreEqual(expectedEncryptedBytes, bytes);
        }


        private static void AssertDecryptedBytesAreCorrect(byte[] expectedDecryptedBytes, string encryptionKey,
            byte[] encryptedBytes)
        {
             var bytes = InternalContentCryptor.DecryptBytes(encryptionKey,encryptedBytes);
            CollectionAssert.AreEqual(expectedDecryptedBytes, bytes);
        }
    }
}