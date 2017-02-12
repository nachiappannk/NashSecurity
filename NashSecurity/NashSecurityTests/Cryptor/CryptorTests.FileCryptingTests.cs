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
            public FileCryptingTests(Type loginHelperType) : base(loginHelperType)
            {
            }

            private static readonly object GuardObject = new object();

            [Test]
            public void EncryptFile()
            {
                var plainFile = "file.txt";
                var encryptedFile = "file.txt.nsec";
                byte[] inputBytes = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

                using (new MultipleEnsurer(new SingleTestEnsurer(), new FilePresenceEnsurer(plainFile, inputBytes),
                        new FileAbsenceEnsurer(encryptedFile)))
                {
                    Cryptor.EncryptFile(SessionToken, plainFile, encryptedFile);
                    Assert.True(File.Exists(encryptedFile));
                    AssertFileBytes(encryptedFile, InternalContentCryptor.EncryptBytes(AccountInfo.MasterPassword,
                        inputBytes));
                }
            }

            private void AssertFileBytes(string encryptedFile, byte[] expectedEncryptedBytes)
            {
                byte[] encryptedBytes = ReadFileBytes(encryptedFile);

                CollectionAssert.AreEqual(expectedEncryptedBytes, encryptedBytes);
            }

            [Test, ExpectedException(typeof(FileDoesNotExistsException))]
            public void EncryptFileNonExisting()
            {
                var plainFile = "file.txt";
                var encryptedFile = "file.txt.nsec";
                using (new MultipleEnsurer(new SingleTestEnsurer(), new FileAbsenceEnsurer(plainFile),
                        new FileAbsenceEnsurer(encryptedFile)))
                {                    
                    Cryptor.EncryptFile(SessionToken, plainFile, encryptedFile);
                }
            }

            [Test, ExpectedException(typeof(UnableToCreateOutputFileException))]
            public void EncryptFileWithUnableToWriteOutputFile()
            {
                byte[] inputBytes = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
                var plainFile = "file.txt";
                var encryptedFile = "file.txt.nsec";

                using (new MultipleEnsurer(new SingleTestEnsurer(), new FilePresenceEnsurer(plainFile, inputBytes),
                    new UnaccessibleFileEnsurer(encryptedFile)))
                {
                    Cryptor.EncryptFile(SessionToken, plainFile, encryptedFile);
                }

            }

            [Test, ExpectedException(typeof(FileDoesNotExistsException))]
            public void DecryptFileNonExisting()
            {
                var plainFile = "file.txt";
                var encryptedFile = "file.txt.nsec";
                using (new MultipleEnsurer(new SingleTestEnsurer(), new FileAbsenceEnsurer(encryptedFile), 
                                            new FileAbsenceEnsurer(plainFile)))
                {
                    Cryptor.DecryptFile(SessionToken, encryptedFile, plainFile);
                }
            }

            public class MultipleEnsurer : IDisposable
            {
                private readonly IDisposable[] _disposables;

                public MultipleEnsurer(params IDisposable[] disposables)
                {
                    _disposables = disposables;
                }

                public void Dispose()
                {
                    foreach (var disposable in _disposables)
                    {
                        disposable.Dispose();
                    }
                }
            }

            public class SingleTestEnsurer : IDisposable
            {
                public SingleTestEnsurer()
                {
                    Monitor.Enter(GuardObject); 
                }

                public void Dispose()
                {
                    Monitor.Exit(GuardObject);
                }
            }

            public class FilePresenceEnsurer : IDisposable
            {
                private readonly string _fileName;
                public FilePresenceEnsurer(string fileName, byte[] dataBytes)
                {
                    _fileName = fileName;
                    CreateFileWithBytes(fileName, dataBytes);
                }
                public void Dispose()
                {
                    DeleteFileIfExists(_fileName);
                }
            }

            public class FileAbsenceEnsurer : IDisposable
            {
                private readonly string _fileName;
                public FileAbsenceEnsurer(string fileName)
                {
                    _fileName = fileName;
                    DeleteFileIfExists(_fileName);
                }
                public void Dispose()
                {
                    DeleteFileIfExists(_fileName);
                }
            }

            public class UnaccessibleFileEnsurer : IDisposable
            {
                private readonly string _fileName;
                private FileStream _fileStream;

                public UnaccessibleFileEnsurer(string fileName)
                {
                    _fileName = fileName;
                    DeleteFileIfExists(_fileName);
                    _fileStream = File.Open(fileName, FileMode.Create);
                }

                public void Dispose()
                {
                    _fileStream.Close();
                    _fileStream.Dispose();
                    DeleteFileIfExists(_fileName);
                }
            }

            private byte[] ReadFileBytes(string encryptedFile)
            {
                return File.ReadAllBytes(encryptedFile);
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