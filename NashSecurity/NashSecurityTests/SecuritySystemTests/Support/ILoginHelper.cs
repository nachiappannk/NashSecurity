namespace NashSecurity.Tests.SecuritySystemTests.Support
{
    public interface ILoginHelper
    {
        ISessionToken Login(ISecuritySystem securitySystem, MockedAccountDataGateway mockedAccountDataGateway,
            AccountInfo accountInfo);
    }
}