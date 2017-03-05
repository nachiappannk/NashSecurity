using NUnit.Framework;

namespace NashSecurity.Tests.InternalCryptorTests.InternalContentCryptorTests
{
    public partial class InternalContentCryptorTests
    {
        public class SimpleTests
        {
            public class TestData
            {
                public byte[] PlainBytes { get; set; }
                public string EncryptionKey { get; set; }
                public byte[] EncryptedBytes { get; set; }
            }

            private readonly TestData[] _testDataArray = new TestData[]
            {
                new TestData()
                {
                    PlainBytes = new byte[] { 12, 3, 4, 5, 12, 31, 43, 90, 59, 212, 234, 34, 1, 0, 0, 0, 1, 2, 3, 4 },
                    EncryptionKey = "Good,Bad,Evil",
                    EncryptedBytes = new byte[] { 214, 210, 233, 243, 217, 95, 95, 18, 252, 188, 185, 9, 136, 30,
                        233, 168, 120, 218, 36, 12, 45, 63, 180, 185, 27, 163, 2, 60, 90, 185, 250, 148 },
                }
            };

            [TestCaseSource("_testDataArray")]
            public void Encrypt(TestData testData)
            {
                AssertEncryptedBytesAreCorrect(
                    testData.EncryptedBytes,
                    testData.EncryptionKey,
                    testData.PlainBytes);

            }

            [TestCaseSource("_testDataArray")]
            public void Decrypt(TestData testData)
            {
                AssertDecryptedBytesAreCorrect(
                    testData.PlainBytes,
                    testData.EncryptionKey,
                    testData.EncryptedBytes);
            }
        }
    }
}