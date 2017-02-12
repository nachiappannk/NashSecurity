using System;
using NashSecurity.AccountData;
using NashSecurity.InternalCryptor;

namespace NashSecurity
{
    public class SecuritySystem
    {
        private readonly IAccountDataGateway _accountDataGateway;
        private readonly SessionDataHolder _sessionDataHolder = new SessionDataHolder();

        public SecuritySystem(IAccountDataGateway accountDataGateway)
        {
            _accountDataGateway = accountDataGateway;
        }

        public ISessionToken SignUp(string userName, string masterPassword, string loginPassword)
        {
            try
            {
                var sessionToken = _sessionDataHolder.CreateSession(userName, masterPassword);
                var encryptedAccount = CreateEncryptedAccount(userName, masterPassword, loginPassword);
                _accountDataGateway.StoreEncryptedAccount(encryptedAccount);
                return sessionToken;
            }
            // ReSharper disable once UnusedVariable
            catch (DataGatewayAccountExistException e)
            {
                throw new AccountNameAlreadyTakenException();
            }
        }

        public ISessionToken SignIn(string userName, string loginPassword)
        {
            try
            {
                var encryptedAccount = _accountDataGateway.ReadEncryptedAccount(userName);
                var passwordCryptor = new InternalPasswordCryptor(loginPassword);
                var encryptedLoginPassword = passwordCryptor.EncryptPassword(loginPassword);
                if (AreBytesEqual(encryptedLoginPassword,encryptedAccount.EncryptedLoginPassword))
                {
                    var decryptPassword = passwordCryptor.DecryptPassword(encryptedAccount.EncryptedMasterPassword);
                    return _sessionDataHolder.CreateSession(userName, decryptPassword);
                }
                throw new AccountOrPasswordIsIncorrectException();
            }
            // ReSharper disable once UnusedVariable
            catch (DataGatewayAccountDoesNotExistException e)
            {
                throw new AccountOrPasswordIsIncorrectException();
            }
        }

        public void Logout(ISessionToken sessionToken)
        {
            _sessionDataHolder.DeleteSession(sessionToken);
        }

        public ICryptor GetCryptor(ISessionToken sessionToken)
        {
            AssetCallIsFromCorrectClient(sessionToken);
            return new Cryptor(_sessionDataHolder);
        }

        private void AssetCallIsFromCorrectClient(ISessionToken sessionToken)
        {
            _sessionDataHolder.AssetCallIsFromCorrectClient(sessionToken);
        }

        private static bool AreBytesEqual(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1.Length != bytes2.Length) return false;
            for (int i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] != bytes2[i]) return false;
            }
            return true;
        }

        private static EncryptedAccount CreateEncryptedAccount(string userName, string masterPassword, string loginPassword)
        {
            var passwordEncrypter = new InternalPasswordCryptor(loginPassword);
            var encryptedAccount = new EncryptedAccount()
            {
                UserName = userName,
                EncryptedLoginPassword = passwordEncrypter.EncryptPassword(loginPassword),
                EncryptedMasterPassword = passwordEncrypter.EncryptPassword(masterPassword),
            };
            return encryptedAccount;
        }

        public class AccountOrPasswordIsIncorrectException : ApplicationException
        {
        }

        public class AccountNameAlreadyTakenException : ApplicationException
        {
        }

        public class SessionIsInvalidException : ApplicationException
        {
        }

        public class AlreadyLoggedInException : ApplicationException
        {
        }

        public class NotLoggedInException : ApplicationException
        {
        }
    }
}
