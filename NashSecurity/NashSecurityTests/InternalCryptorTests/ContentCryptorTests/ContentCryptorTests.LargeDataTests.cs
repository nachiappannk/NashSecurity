using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace NashSecurity.Tests.InternalCryptorTests.ContentCryptorTests
{
    public partial class ContentCryptorTests
    {
        public class LargeDataTests
        {
            private const string EncryptionKey = " 213 G ood !@";
            private const string EncryptedResourceFile =
                "NashSecurity.Tests.InternalCryptorTests.ContentCryptorTests.EncryptedTestFile.jpg.nsec";
            private const string PlainResourceFile =
                "NashSecurity.Tests.InternalCryptorTests.ContentCryptorTests.TestFile.jpg";

            [Test]
            public void Encrypt()
            {
                AssertEncryptedBytesAreCorrect( ReadByteFromEmbeddedResource(EncryptedResourceFile),
                    EncryptionKey,
                    ReadByteFromEmbeddedResource(PlainResourceFile));
            }

            [Test]
            public void Decrypt()
            {

                AssertDecryptedBytesAreCorrect(ReadByteFromEmbeddedResource(PlainResourceFile),
                        EncryptionKey,
                        ReadByteFromEmbeddedResource(EncryptedResourceFile));
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