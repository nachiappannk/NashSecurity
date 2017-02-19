using System;
using System.Linq;
using System.Text.RegularExpressions;
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
            _accountDataGateway = new ExceptionHandingAccountDataGateway(accountDataGateway);
        }

        public ISessionToken SignUp(string userName, string masterPassword, string loginPassword)
        {
            AssertNotLoggedIn();
            AssertUserNameIsValidForSignUp(userName);
            var encryptedAccount = CreateEncryptedAccount(userName, masterPassword, loginPassword);
            _accountDataGateway.StoreEncryptedAccount(encryptedAccount);
            return _sessionDataHolder.CreateSession(userName, masterPassword);
        }

        public ISessionToken SignIn(string userName, string loginPassword)
        {
            AssertNotLoggedIn();
            AssertAccountExists(userName);
            var encryptedAccount = _accountDataGateway.ReadEncryptedAccount(userName);
            var passwordCryptor = new InternalPasswordCryptor(loginPassword);
            var encryptedLoginPassword = passwordCryptor.EncryptPassword(loginPassword);
            var areBytesEqual = AreBytesEqual(encryptedLoginPassword,encryptedAccount.EncryptedLoginPassword);
            if (areBytesEqual)
            {
                var decryptPassword = passwordCryptor.DecryptPassword(encryptedAccount.EncryptedMasterPassword);
                return _sessionDataHolder.CreateSession(userName, decryptPassword);
            }
            throw new AccountOrPasswordIsIncorrectException();
        }

        public void Logout(ISessionToken sessionToken)
        {
            _sessionDataHolder.AssetCallIsFromCorrectClient(sessionToken);
            _sessionDataHolder.DeleteSession(sessionToken);
        }

        public ICryptor GetCryptor(ISessionToken sessionToken)
        {
            AssetCallIsFromCorrectClient(sessionToken);
            return new Cryptor(_sessionDataHolder);
        }

        public void AssertUserNameIsValidForSignUp(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) 
                throw new UserNameForSignUpIsEmptyException();
            userName = userName.Trim();
            if (userName.Length < 8)
                throw new UserNameForSignUpIsTooShortException();
            const string regExpForAlphaNumericUnderscoreHyphen = @"^[a-zA-Z0-9\-_]*$";
            if (!Regex.IsMatch(userName, regExpForAlphaNumericUnderscoreHyphen))
                throw new UserNameForSignUpHasCharactersNotAllowedException();
            if(_accountDataGateway.DoesAccountExist(userName))
                throw new AccountNameAlreadyTakenException();
        }

        public void AssertMasterPasswordIsValidForSignUp(string masterPassword)
        {
            if(masterPassword == null) throw new MasterPasswordIsShortException();
            if(masterPassword.Length <16) throw new MasterPasswordIsShortException();
        }

        public void AssertLoginPasswordIsValidForSignUp(string logiPassword)
        {
            if (logiPassword == null) throw new LoginPasswordIsShortException();
            if (logiPassword.Length < 8) throw new LoginPasswordIsShortException();
        }

        private void AssertAccountExists(string userName)
        {
            if (!_accountDataGateway.DoesAccountExist(userName)) throw new AccountOrPasswordIsIncorrectException();
        }

        private void AssetCallIsFromCorrectClient(ISessionToken sessionToken)
        {
            _sessionDataHolder.AssetCallIsFromCorrectClient(sessionToken);
        }

        private void AssertNotLoggedIn()
        {
            _sessionDataHolder.AssertNotLoggedIn();
        }

        private static bool AreBytesEqual(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1.Length != bytes2.Length) return false;
            return !bytes1.Where((byte1, i) => byte1 != bytes2[i]).Any();
        }

        private static EncryptedAccount CreateEncryptedAccount(string userName, string masterPassword, 
            string loginPassword)
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

        public class UserNameForSignUpIsEmptyException : ApplicationException
        {
        }

        public class UserNameForSignUpIsTooShortException : ApplicationException
        {
        }

        public class UserNameForSignUpHasCharactersNotAllowedException : ApplicationException
        {
        }

        public class MasterPasswordIsShortException : ApplicationException
        {
        }

        public class LoginPasswordIsShortException : ApplicationException
        {
        }

        public class DataGatewayException : ApplicationException
        {
            public DataGatewayException(string message, Exception exception): base(message,exception)
            {

            }
        }
    }
}
