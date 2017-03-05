using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.Scenario
{
    public interface IHasExitedData
    {
        MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        ISecuritySystem SecuritySystem { get; set; }
        AccountInfo AccountInfo { get; set; }
        ISessionToken InvalidSessionToken { get; set; }
    }
}