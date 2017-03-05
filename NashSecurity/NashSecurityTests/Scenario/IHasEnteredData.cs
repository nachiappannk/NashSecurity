using NashSecurity.Tests.SecuritySystemTests.Support;

namespace NashSecurity.Tests.Scenario
{
    public interface IHasEnteredData
    {
        ISessionToken SessionToken { get; set; }
        ISecuritySystem SecuritySystem { get; set; }
        MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        AccountInfo AccountInfo { get; set; }
    }
}