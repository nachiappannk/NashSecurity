using System;
using System.Linq;
using NUnit.Framework;

namespace NashSecurity.Tests
{
    [TestFixture]
    public class AesCryptorTests
    {
        
        [TestCaseSource("TestData")]
        public void Encrypt(EncryptionDecryptionTestData testData)
        {
            var encryptedOutput = AesCryptor.Encrypt(testData.PlainData, testData.MainPassBytes,
                testData.SaltPassBytes, testData.Iteration);

            {
                var encryptedDataAsString = encryptedOutput.Select(s => s.ToString());
                var output = String.Join(", ", encryptedDataAsString);
                Console.WriteLine(testData.ErrorMessage);
                Console.WriteLine(output);
            }
            CollectionAssert.AreEqual(testData.EncryptedData, encryptedOutput, testData.ErrorMessage);
        }


        [TestCaseSource("TestData")]
        public void Decrypt(EncryptionDecryptionTestData testData)
        {
            var encryptedOutput = AesCryptor.Decrypt(testData.EncryptedData, testData.MainPassBytes,
                testData.SaltPassBytes, testData.Iteration);
            CollectionAssert.AreEqual(testData.PlainData, encryptedOutput);
        }


        public static readonly EncryptionDecryptionTestData[] TestData =
        {
            new EncryptionDecryptionTestData()
            {
                ErrorMessage = "Test1",
                PlainData = new byte[]{5,4,3,6,1,2},
                MainPassBytes = new byte[]{83,111,109,101,32,112,97,115,115,119,111,114,100},
                SaltPassBytes = new byte[]{83,97,108,116,32,112,97,115,115,119,111,114,100},
                Iteration = 67,
                EncryptedData = new byte[]{44, 88, 20, 81, 149, 9, 136, 75, 13, 211, 138, 73, 39, 224, 45, 145},
            },

            new EncryptionDecryptionTestData()
            {
                ErrorMessage = "Test2: Empty Main Password",
                PlainData = new byte[]{5,4,3,6,1,2},
                MainPassBytes = new byte[]{},
                SaltPassBytes = new byte[]{83,97,108,116,32,112,97,115,115,119,111,114,100},
                Iteration = 67,
                EncryptedData = new byte[]{255, 209, 27, 233, 56, 39, 219, 114, 80, 212, 29, 178, 254, 55, 91, 108},
            },

            new EncryptionDecryptionTestData()
            {
                ErrorMessage = "Test3: Empty Salt Password",
                PlainData = new byte[]{5,4,3,6,1,2},
                MainPassBytes = new byte[]{83,111,109,101,32,112,97,115,115,119,111,114,100},
                SaltPassBytes = new byte[]{},
                Iteration = 67,
                EncryptedData = new byte[]{80, 157, 123, 136, 170, 215, 88, 101, 27, 47, 191, 201, 195, 63, 44, 194},
            },

            new EncryptionDecryptionTestData()
            {
                ErrorMessage = "Test4: Iteration is 1",
                PlainData = new byte[]{5,4,3,6,1,2},
                MainPassBytes = new byte[]{83,111,109,101,32,112,97,115,115,119,111,114,100},
                SaltPassBytes = new byte[]{83,111,109,101,32,115,97,108,116,32,112,97,115,115,119,111,114,100},
                Iteration = 1,
                EncryptedData = new byte[]{7, 181, 114, 20, 79, 238, 223, 251, 31, 62, 39, 128, 85, 120, 69, 129},
            },
        };


        public class EncryptionDecryptionTestData
        {
            public byte[] PlainData { get; set; }
            public byte[] MainPassBytes { get; set; }
            public byte[] SaltPassBytes { get; set; }
            public int Iteration { get; set; }
            public byte[] EncryptedData { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
