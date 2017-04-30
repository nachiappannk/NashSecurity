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

    public interface IHasSessionToken
    {
        ISessionToken SessionToken { get; set; }
    }

    public interface IHasMockedAccountDataGateway
    {
        MockedAccountDataGateway MockedAccountDataGateway { get; set; }
    }

    public interface IHasSecuritySystem
    {
        ISecuritySystem SecuritySystem { get; set; }
    }

    public interface IHasAccountInfo
    {
        AccountInfo AccountInfo { get; set; }
    }

    public interface IHasInvalidSessionToken
    {
        ISessionToken InvalidSessionToken { get; set; }
    }
}