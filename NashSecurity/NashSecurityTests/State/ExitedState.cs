using NashLink;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.State
{
    public class ExitedState : IHasExitedData, IState, IHasPreviousState
    {
        public ISessionToken InvalidSessionToken { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public ExitedState(IState previousState)
        {
            previousState.CopyPossibleProperties(this);
            SecuritySystem.Logout(((IHasEnteredData)previousState).SessionToken);
            InvalidSessionToken = ((IHasEnteredData)previousState).SessionToken;
        }

        public IState PreviousState { get; set; }
    }
}