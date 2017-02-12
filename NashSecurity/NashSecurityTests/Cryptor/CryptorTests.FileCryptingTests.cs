using System;
using System.IO;
using System.Threading;
using NashSecurity.InternalCryptor;
using NUnit.Framework;

namespace NashSecurity.Tests.Cryptor
{
    public partial class CryptorTests
    {
        public class FileCryptingTests : CryptorTests
        {
            private static readonly object GuardObject = new object();
            private static string _plainFile;
            private static string _encryptedFile;
            private static byte[] _plainBytes;
            private static byte[] _encryptedBytes;

            public FileCryptingTests(Type loginHelperType) : base(loginHelperType)
            {
                _plainFile = "file.txt";
                _encryptedFile = "file.txt.nsec";
                _plainBytes = new byte[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
                _encryptedBytes = InternalContentCryptor.EncryptBytes(AccountInfo.MasterPassword,
                        _plainBytes);
            }

            [Test]
            public void EncryptFileTest()
            {
                using (new TestScopeProvider())
                {
                    CreatePlainFile();
                    EncryptFile();
                    AssertEncryptedFileIsCorrect();
                }
            }

            [Test, ExpectedException(typeof(FileDoesNotExistsException))]
            public void EncryptFileNonExisting()
            {
                using (new TestScopeProvider())
                {                    
                    EncryptFile();
                }
            }

            [Test, ExpectedException(typeof(UnableToCreateOutputFileException))]
            public void EncryptFileWithUnableToWriteOutputFile()
            {
                using (new TestScopeProvider())
                {
                    using (new FileAccessPreventer(_encryptedFile))
                    {
                        CreatePlainFile();
                        EncryptFile();
                    }
                }
            }

            [Test, ExpectedException(typeof(UnableToCreateOutputFileException))]
            public void EncryptFileOutputFileHidden()
            {
                using (new TestScopeProvider())
                {
                    using (new FileHider(_encryptedFile))
                    {
                        CreatePlainFile();
                        EncryptFile();
                    }
                }
            }

            [Test]
            public void DecryptFileTest()
            {
                using (new TestScopeProvider())
                {
                    CreateEncryptedFile();
                    DecryptFile();
                    AssertPlainFileIsCorrect();
                }
            }

            [Test, ExpectedException(typeof(FileDoesNotExistsException))]
            public void DecryptFileNonExisting()
            {
                using (new TestScopeProvider())
                {
                    DecryptFile();
                }
            }

            [Test, ExpectedException(typeof(UnableToCreateOutputFileException))]
            public void DecryptFileWithUnableToWriteOutputFile()
            {
                using (new TestScopeProvider())
                {
                    using (new FileAccessPreventer(_plainFile))
                    {
                        CreateEncryptedFile();
                        DecryptFile();
                    }
                }
            }

            [Test, ExpectedException(typeof(UnableToCreateOutputFileException))]
            public void DecryptFileOutputFileHidden()
            {
                using (new TestScopeProvider())
                {
                    using (new FileHider(_plainFile))
                    {
                        CreateEncryptedFile();
                        DecryptFile();
                    }
                }
            }

            public class TestScopeProvider : IDisposable
            {
                public TestScopeProvider()
                {

                    Monitor.Enter(GuardObject); 
                }

                public void Dispose()
                {
                    DeleteFileIfExists(_plainFile);
                    DeleteFileIfExists(_encryptedFile);
                    Monitor.Exit(GuardObject);
                }
            }

            public class FileAccessPreventer : IDisposable
            {
                private FileStream _fileStream;
                public FileAccessPreventer(string fileName)
                {
                    _fileStream = File.Open(fileName, FileMode.Create);
                }

                public void Dispose()
                {
                    _fileStream.Close();
                    _fileStream.Dispose();
                }
            }

            public class FileHider : IDisposable
            {
                private readonly string _fileName;
                private FileInfo _fileInfo;

                public FileHider(string fileName)
                {
                    _fileName = fileName;
                    File.WriteAllBytes(_fileName, new byte[]{0});
                    _fileInfo = new FileInfo(Environment.CurrentDirectory + @"\" + _fileName);
                    _fileInfo.Attributes |= FileAttributes.Hidden;
                    
                }

                public void Dispose()
                {
                    _fileInfo.Attributes &= ~FileAttributes.Hidden;
                    DeleteFileIfExists(_fileName);
                }
            }

            private static void CreateEncryptedFile()
            {
                CreateFileWithBytes(_encryptedFile, _encryptedBytes);
            }

            private static void CreatePlainFile()
            {
                CreateFileWithBytes(_plainFile, _plainBytes);
            }

            private void DecryptFile()
            {
                Cryptor.DecryptFile(SessionToken, _encryptedFile, _plainFile);
            }

            private void EncryptFile()
            {
                Cryptor.EncryptFile(SessionToken, _plainFile, _encryptedFile);
            }

            private void AssertEncryptedFileIsCorrect()
            {
                Assert.True(File.Exists(_encryptedFile));
                AssertFileConentIsCorrect(_encryptedFile, _encryptedBytes);
            }

            private void AssertPlainFileIsCorrect()
            {
                Assert.True(File.Exists(_plainFile));
                AssertFileConentIsCorrect(_plainFile, _plainBytes);
            }

            private void AssertFileConentIsCorrect(string fileName, byte[] fileDataBytes)
            {
                byte[] encryptedBytes = File.ReadAllBytes(fileName);
                CollectionAssert.AreEqual(fileDataBytes, encryptedBytes);
            }

            private static void CreateFileWithBytes(string inputFile, byte[] inputBytes)
            {
                if (File.Exists(inputFile)) File.Delete(inputFile);
                File.WriteAllBytes(inputFile, inputBytes);
            }

            private static void DeleteFileIfExists(string inputFile)
            {
                if (File.Exists(inputFile)) File.Delete(inputFile);
            }
        }
    }
}