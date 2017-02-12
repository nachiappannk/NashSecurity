namespace NashSecurity.Tests.SecuritySystemTests.Support
{
    public interface ILoginHelper
    {
        ISessionToken Login(SecuritySystem securitySystem, MockedAccountDataGateway mockedAccountDataGateway,
            AccountInfo accountInfo);
    }
}