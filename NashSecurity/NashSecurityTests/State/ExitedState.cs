using NashLink;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.State
{
    public class ExitedState : IHasExitedData
    {
        public ISessionToken InvalidSessionToken { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public ExitedState(IHasEnteredData hasEnteredData)
        {
            hasEnteredData.CopyPossibleProperties(this);
            SecuritySystem.Logout(hasEnteredData.SessionToken);
            InvalidSessionToken = hasEnteredData.SessionToken;
        }
    }
}