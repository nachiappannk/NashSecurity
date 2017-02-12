using System;
using System.IO;

namespace NashSecurity
{
    public interface ICryptor
    {
        byte[] Encrypt(ISessionToken sessionToken, byte[] rawBytes);
        byte[] Decrypt(ISessionToken sessionToken, byte[] encryptedBytes);
    }

    public static class FileCryptorExtentions
    {
        public static void EncryptFile(this ICryptor cryptor, ISessionToken sessionToken, 
            string inputFile, string outputFile)
        {
            CryptFile(sessionToken, inputFile, outputFile, cryptor.Encrypt);
        }

        public static void DecryptFile(this ICryptor cryptor, ISessionToken sessionToken,
            string encryptedFile, string dataFile)
        {
            CryptFile(sessionToken, encryptedFile, dataFile, cryptor.Decrypt);
        }

        public static void CryptFile(ISessionToken sessionToken, string inputFile, string outputFile,
            Func<ISessionToken, byte[], byte[]> cryptingFunction)
        {
            AssertFileExists(inputFile);
            var inputBytes = File.ReadAllBytes(inputFile);
            var outputBytes = cryptingFunction(sessionToken, inputBytes);
            try
            {
                File.WriteAllBytes(outputFile, outputBytes);
            }
            catch (Exception e)
            {
                throw new UnableToCreateOutputFileException(e);
            }
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