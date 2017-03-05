using System;
using System.Collections.Generic;
using System.Linq;
using NashSecurity.AccountData;

namespace NashSecurity.Tests.SecuritySystemTests.Support
{
    public class MockedAccountDataGateway : IAccountDataGateway
    {
        public event Action<EncryptedAccount> StoreEncryptedAccountCalled;
        public event Action<string> DeleteEncryptedAccountCalled;
        public event Action<EncryptedAccount> UpdatEncryptedAccountCalled;

        Dictionary<string,EncryptedAccount> _userName2AccountsLookUp = new Dictionary<string,EncryptedAccount>();

        public IList<string> GetUserNames()
        {
            return _userName2AccountsLookUp.Keys.ToList();
        }

        public void StoreEncryptedAccount(EncryptedAccount encryptedAccount)
        {
            if(this.DoesAccountExist(encryptedAccount.UserName)) throw new Exception();
            _userName2AccountsLookUp.Add(encryptedAccount.UserName,encryptedAccount);
            FireStoreEncryptedAccountCalled(encryptedAccount);
        }

        public void DeleteEncryptedAccount(string userName)
        {
            throw new NotImplementedException();
        }

        public EncryptedAccount ReadEncryptedAccount(string userName)
        {
            if (!this.DoesAccountExist(userName)) throw new Exception();
            return _userName2AccountsLookUp[userName];
        }

        public void UpdatEncryptedAccount(EncryptedAccount encryptedAccount)
        {
            throw new NotImplementedException();
        }

        protected virtual void FireStoreEncryptedAccountCalled(EncryptedAccount obj)
        {
            var handler = StoreEncryptedAccountCalled;
            if (handler != null) handler(obj);
        }

        protected virtual void FireUpdatEncryptedAccountCalled(EncryptedAccount obj)
        {
            var handler = UpdatEncryptedAccountCalled;
            if (handler != null) handler(obj);
        }

        protected virtual void FireDeleteEncryptedAccountCalled(string obj)
        {
            var handler = DeleteEncryptedAccountCalled;
            if (handler != null) handler(obj);
        }
    }
}