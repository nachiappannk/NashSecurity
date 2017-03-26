using NashLink;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.State
{
    public class ExitedState 
        : IState, IHasPreviousState,
          IHasMockedAccountDataGateway, IHasSecuritySystem, IHasAccountInfo, IHasInvalidSessionToken
    {
        public ISessionToken InvalidSessionToken { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }

        private ExitedState()
        {
        }

        public static ExitedState Create<T>(T previousState)
            where T : IState, IHasSessionToken, IHasMockedAccountDataGateway, IHasSecuritySystem, IHasAccountInfo
        {
            var state = new ExitedState();
            previousState.CopyPossibleProperties(state);
            state.SecuritySystem.Logout(previousState.SessionToken);
            state.InvalidSessionToken = previousState.SessionToken;
            return state;
        }

        public IState PreviousState { get; set; }
    }
}