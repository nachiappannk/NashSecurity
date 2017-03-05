using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.Scenario
{
    public interface IHasCreatedData
    {
        MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        ISecuritySystem SecuritySystem { get; set; }
        AccountInfo AccountInfo { get; set; }
    }
}