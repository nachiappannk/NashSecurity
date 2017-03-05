namespace NashSecurity.Tests.SecuritySystemTests.Support
{
    public static class SecuritySystemTextExtentions
    {
        public static ISessionToken SignUp(this ISecuritySystem securitySystem, AccountInfo accountInfo)
        {
            return securitySystem.SignUp(accountInfo.AccountName, accountInfo.MasterPassword, accountInfo.LoginPassword);
        }

        public static ISessionToken SignIn(this ISecuritySystem securitySystem, AccountInfo accountInfo)
        {
            return securitySystem.SignIn(accountInfo.AccountName, accountInfo.LoginPassword);
        }
    }
}