using System;
using System.IO;
using NashSecurity.InternalCryptor;

namespace NashSecurity
{
    public interface ICryptor
    {
        byte[] Encrypt(ISessionToken sessionToken, byte[] rawBytes);
        byte[] Decrypt(ISessionToken sessionToken, byte[] encryptedBytes);
    }

    public static class FileCryptorExtentions
    {
        static FileCryptorExtentions()
        {
        }

        

        public static void EncryptFile(this ICryptor cryptor, ISessionToken sessionToken, 
            string inputFile, string outputFile)
        {
            AssertFileExists(inputFile);
            var inputBytes = File.ReadAllBytes(inputFile);
            var outputBytes = cryptor.Encrypt(sessionToken, inputBytes);
            try
            {
                File.WriteAllBytes(outputFile, outputBytes);
            }
            catch (Exception e)
            {
                throw new UnableToCreateOutputFileException(e);
            }
        }

        public static void DecryptFile(this ICryptor cryptor, ISessionToken sessionToken,
            string encryptedFile, string dataFile)
        {
            AssertFileExists(encryptedFile);
        }

        private static void AssertFileExists(string encryptedFile)
        {
            if (!File.Exists(encryptedFile)) throw new FileDoesNotExistsException();
        }
    }

    public class FileDoesNotExistsException : ApplicationException
    {
    }

    public class UnableToCreateOutputFileException : ApplicationException
    {
        public UnableToCreateOutputFileException(Exception innerException) : base(string.Empty,innerException)
        {
        }
    }
}