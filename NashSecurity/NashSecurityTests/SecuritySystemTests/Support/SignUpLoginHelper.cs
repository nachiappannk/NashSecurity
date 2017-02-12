namespace NashSecurity.Tests.SecuritySystemTests.Support
{
    public class SignUpLoginHelper : ILoginHelper
    {
        public ISessionToken Login(SecuritySystem securitySystem, MockedAccountDataGateway mockedAccountDataGateway, 
            AccountInfo accountInfo)
        {
            return securitySystem.SignUp(accountInfo);
        }
    }
}