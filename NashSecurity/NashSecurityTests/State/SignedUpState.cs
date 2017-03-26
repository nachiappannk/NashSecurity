using NashLink;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.State
{
    public class SignedUpState : IHasEnteredData
    {
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public SignedUpState()
        {
            var createdState = new CreatedState();
            createdState.CopyPossibleProperties(this);
            SessionToken = SecuritySystem.SignUp(AccountInfo);
        }
    }
}