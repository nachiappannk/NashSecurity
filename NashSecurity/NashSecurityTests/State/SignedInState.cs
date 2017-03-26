using NashLink;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.State
{
    public class SignedInState : IHasEnteredData, IState
    {
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public SignedInState()
        {
            var exitedState = new ExitedState(new SignedUpState());
            exitedState.CopyPossibleProperties(this);
            SessionToken = SecuritySystem.SignIn(AccountInfo);
        }
    }
}