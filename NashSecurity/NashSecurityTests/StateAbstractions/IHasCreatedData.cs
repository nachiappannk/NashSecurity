using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.StateAbstractions
{
    public interface IHasCreatedData
    {
        MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        ISecuritySystem SecuritySystem { get; set; }
        AccountInfo AccountInfo { get; set; }
    }
}