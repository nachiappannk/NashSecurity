using System;
using System.Collections.Generic;
using NashSecurity.AccountData;

namespace NashSecurity
{
    public class ExceptionHandingAccountDataGateway : IAccountDataGateway
    {
        private readonly IAccountDataGateway _accountDataGateway;

        public ExceptionHandingAccountDataGateway(IAccountDataGateway accountDataGateway)
        {
            _accountDataGateway = accountDataGateway;
        }
        public IList<string> GetUserNames()
        {
            try
            {
                return _accountDataGateway.GetUserNames();
            }
            catch (Exception e)
            {
                throw new SecuritySystem.DataGatewayException("",e);
            }
        }

        public void StoreEncryptedAccount(EncryptedAccount encryptedAccount)
        {
            try
            {
                _accountDataGateway.StoreEncryptedAccount(encryptedAccount);
            }
            catch (Exception e)
            {
                throw new SecuritySystem.DataGatewayException("", e);
            }
        }

        public void DeleteEncryptedAccount(string userName)
        {
            try
            {
                _accountDataGateway.DeleteEncryptedAccount(userName);
            }
            catch (Exception e)
            {
                throw new SecuritySystem.DataGatewayException("", e);
            }
        }

        public EncryptedAccount ReadEncryptedAccount(string userName)
        {
            try
            {
                return _accountDataGateway.ReadEncryptedAccount(userName);
            }
            catch (Exception e)
            {
                throw new SecuritySystem.DataGatewayException("", e);
            }
        }

        public void UpdatEncryptedAccount(EncryptedAccount encryptedAccount)
        {
            try
            {
                _accountDataGateway.UpdatEncryptedAccount(encryptedAccount);
            }
            catch (Exception e)
            {
                throw new SecuritySystem.DataGatewayException("", e);
            }
        }
    }
}