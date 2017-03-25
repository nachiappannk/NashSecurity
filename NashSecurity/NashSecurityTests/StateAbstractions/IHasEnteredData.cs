using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.StateAbstractions
{
    public interface IHasEnteredData
    {
        ISessionToken SessionToken { get; set; }
        ISecuritySystem SecuritySystem { get; set; }
        MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        AccountInfo AccountInfo { get; set; }
    }
}