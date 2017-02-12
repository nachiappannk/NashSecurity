using System.Diagnostics;
using NashSecurity.InternalCryptor;
using NUnit.Framework;

namespace NashSecurity.Tests.InternalCryptorTests
{
    [TestFixture]
    public class PasswordCryptorTests
    {
        public class TestData
        {
            public string TestIdentifier { get; set; }
            public string Key { get; set; }
            public string Password { get; set; }
            public byte[] EncryptedPassword { get; set; }
        }

        private readonly TestData[] _testDataArray = {
            new TestData()
            {
                TestIdentifier = "Test1",
                Key = "LoginPassword",
                Password = "LoginPassword",
                EncryptedPassword = new byte[]{127,141,224,102,242,131,13,61,93,159,22,111,15,126,29,28}
            },

            new TestData()
            {
                TestIdentifier = "Test2",
                Key = "LoginPassword",
                Password = "This is good!@#",
                EncryptedPassword = new byte[]{41,229,210,236,107,250,44,218,151,210,198,68,99,234,140,94}
            },

            new TestData()
            {
                TestIdentifier = "Test3",
                Key = "Login Password",
                Password = "This is good!@#",
                EncryptedPassword = new byte[]{220,208,107,132,137,153,186,40,111,3,32,13,69,16,67,109}
            },

            new TestData()
            {
                TestIdentifier = "Test4",
                Key = "W3lc0m32017@S!3m3ns",
                Password = "W3lc0m32017@S!3m3ns",
                EncryptedPassword = new byte[]{11,48,62,123,50,116,109,13,32,182,122,224,251,0,180,156,242,93,
                    192,207,46,234,162,136,225,247,89,215,128,84,185,72}
            },

            new TestData()
            {
                TestIdentifier = "Test5",
                Key = "W3lc0m32017 @S!3m3ns",
                Password = "W3lc0m32017 @S!3m3ns",
                EncryptedPassword = new byte[]{20,207,218,176,253,64,231,108,147,231,68,10,98,70,229,89,14,169,115,
                    192,68,115,64,139,147,196,160,13,59,180,34,6}
            },

            new TestData()
            {
                TestIdentifier = "Test6",
                Key = "W3lc0m32017@S!3m3ns",
                Password = "  W3lc0m32017  @S!3m3ns  ",
                EncryptedPassword = new byte[]{244,39,250,28,214,215,8,233,250,120,173,14,15,86,179,32,95,144,212,
                    212,147,40,224,108,110,240,118,233,221,154,115,132}
            },

            new TestData()
            {
                TestIdentifier = "Test7",
                Key = "W3lc0m32017@S!3m3ns",
                Password = "goodbadAndUgly",
                EncryptedPassword = new byte[]{189,228,77,74,106,40,241,135,161,66,37,127,239,255,235,229}
            },
        };

        [TestCaseSource("_testDataArray")]
        public void Encrypt(TestData testData)
        {
            InternalPasswordCryptor internalPasswordCryptor = new InternalPasswordCryptor(testData.Key);
            var bytes = internalPasswordCryptor.EncryptPassword(testData.Password);
            Debug.Print(string.Join(",",bytes));
            CollectionAssert.AreEqual(testData.EncryptedPassword, bytes, testData.TestIdentifier);
        }

        [TestCaseSource("_testDataArray")]
        public void Decrypt(TestData testData)
        {
            InternalPasswordCryptor internalPasswordCryptor = new InternalPasswordCryptor(testData.Key);
            var password = internalPasswordCryptor.DecryptPassword(testData.EncryptedPassword);
            Assert.AreEqual(testData.Password, password);
        }

        [Test]
        public void EncryptThenDecrypt()
        {
            AssertThatEncryptAndDecryptGivesInput("SomeKey", "SomeInputData");
            AssertThatEncryptAndDecryptGivesInput(" ##DDSD GOOD Key 1", "SimpleData");
        }

        private static void AssertThatEncryptAndDecryptGivesInput(string key, string input)
        {
            InternalPasswordCryptor internalPasswordCryptor = new InternalPasswordCryptor(key);
            var bytes = internalPasswordCryptor.EncryptPassword(input);
            var decryptedPassword = internalPasswordCryptor.DecryptPassword(bytes);
            Assert.AreEqual(input, decryptedPassword);
        }
    }
}
