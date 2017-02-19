namespace NashSecurity.Tests.SecuritySystemTests.Support
{
    public class SignUpLoginHelper : ILoginHelper
    {
        public ISessionToken Login(ISecuritySystem securitySystem, MockedAccountDataGateway mockedAccountDataGateway, 
            AccountInfo accountInfo)
        {
            return securitySystem.SignUp(accountInfo);
        }
    }
}