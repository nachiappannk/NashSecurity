namespace NashSecurity.Tests.Support
{
    public class AccountInfo
    {
        public AccountInfo(string accountName, string masterPassword, string loginPassword) 
        {
            MasterPassword = masterPassword;
            AccountName = accountName;
            LoginPassword = loginPassword;
        }
        public string MasterPassword { get; set; }
        public string AccountName { get; set; }
        public string LoginPassword { get; set; }
    }


    public static class AccountInfoExtentions
    {
        public static AccountInfo CorruptMasterPassword(this AccountInfo accountInfo)
        {
            var myAccountInfo = accountInfo.GetCopy();
            myAccountInfo.MasterPassword = "WrongPassword";
            return myAccountInfo;
        }

        public static AccountInfo CorruptLoginPassword(this AccountInfo accountInfo)
        {
            var myAccountInfo = accountInfo.GetCopy();
            myAccountInfo.LoginPassword = "WrongPassword";
            return myAccountInfo;
        }

        public static AccountInfo CorruptUserName(this AccountInfo accountInfo)
        {
            var myAccountInfo = accountInfo.GetCopy();
            myAccountInfo.AccountName = "WrongUserName";
            return myAccountInfo;
        }

        public static AccountInfo GetCopy(this AccountInfo accountInfo)
        {
            return new AccountInfo(accountInfo.AccountName, accountInfo.MasterPassword,
                accountInfo.LoginPassword);
        }
    }

    
}