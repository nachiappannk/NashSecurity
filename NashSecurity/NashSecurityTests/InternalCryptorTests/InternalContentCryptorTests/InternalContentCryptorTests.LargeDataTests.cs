using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace NashSecurity.Tests.InternalCryptorTests.InternalContentCryptorTests
{
    public partial class InternalContentCryptorTests
    {
        public class LargeDataTests
        {
            private const string EncryptionKey = " 213 G ood !@";
            private const string EncryptedResourceFile =
                "NashSecurity.Tests.InternalCryptorTests.ContentCryptorTests.EncryptedTestFile.jpg.nsec";
            private const string PlainResourceFile =
                "NashSecurity.Tests.InternalCryptorTests.ContentCryptorTests.TestFile.jpg";

            private readonly byte[] _plainBytes;
            private readonly byte[] _encryptedBytes;

            public LargeDataTests()
            {
                //_plainBytes = ReadByteFromEmbeddedResource(PlainResourceFile);
                //_encryptedBytes = ReadByteFromEmbeddedResource(EncryptedResourceFile);
            }

            [Test, Ignore]
            public void Encrypt()
            {
                AssertEncryptedBytesAreCorrect(_encryptedBytes,
                    EncryptionKey, _plainBytes);
            }

            [Test, Ignore]
            public void Decrypt()
            {

                AssertDecryptedBytesAreCorrect(_plainBytes,
                        EncryptionKey,
                        _encryptedBytes);
            }

            private static byte[] ReadByteFromEmbeddedResource(string input)
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(input))
                {
                    Debug.Assert(stream != null, "stream != null");
                    using (var reader = new StreamReader(stream))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            reader.BaseStream.CopyTo(memoryStream);
                            return memoryStream.ToArray();
                        }
                    }
                }
            }
        }
    }
}