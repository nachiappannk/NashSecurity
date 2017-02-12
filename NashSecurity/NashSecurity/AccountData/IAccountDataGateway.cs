using System;
using System.Collections.Generic;

namespace NashSecurity.AccountData
{
    public interface IAccountDataGateway
    {
        IList<string> GetUserNames();
        void StoreEncryptedAccount(EncryptedAccount encryptedAccount);
        void DeleteEncryptedAccount(string userName);
        EncryptedAccount ReadEncryptedAccount(string userName);
        void UpdatEncryptedAccount(EncryptedAccount encryptedAccount);
    }

    public static class AccountDataGatewayExtentions
    {
        public static bool DoesAccountExist(this IAccountDataGateway accountDataGateway, string userName)
        {
            return accountDataGateway.GetUserNames().Contains(userName);
        }
    }



    public class DataGatewayAccountDoesNotExistException : ApplicationException
    {
    }

    public class DataGatewayAccountExistException : ApplicationException
    {
    }
}