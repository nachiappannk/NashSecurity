using NashSecurity.Tests.SecuritySystemTests.Support;

namespace NashSecurity.Tests.Context
{
    public interface IExitedSecurityContext
    {
        ISessionToken SessionToken { get; set; }
        MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        ISecuritySystem SecuritySystem { get; set; }
        AccountInfo AccountInfo { get; set; }
    }
}