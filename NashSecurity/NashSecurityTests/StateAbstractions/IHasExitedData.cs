using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.StateAbstractions
{
    public interface IHasExitedData
    {
        MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        ISecuritySystem SecuritySystem { get; set; }
        AccountInfo AccountInfo { get; set; }
        ISessionToken InvalidSessionToken { get; set; }
    }
}