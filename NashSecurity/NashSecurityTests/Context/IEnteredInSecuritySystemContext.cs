using NashSecurity.Tests.SecuritySystemTests.Support;

namespace NashSecurity.Tests.Context
{
    public interface IEnteredInSecuritySystemContext
    {
        ISessionToken SessionToken { get; set; }
        ISecuritySystem SecuritySystem { get; set; }
        MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        AccountInfo AccountInfo { get; set; }
    }
}