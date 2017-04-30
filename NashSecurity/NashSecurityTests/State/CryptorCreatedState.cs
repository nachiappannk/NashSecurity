using NashLink;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.State
{
    public class CryptorCreatedState : IHasCryptorCreatedData, IState, IHasPreviousState
    {
        public CryptorCreatedState(IState previousState)
        {
            PreviousState = previousState;
            PreviousState.CopyPossibleProperties(this);
            Cryptor = SecuritySystem.GetCryptor(SessionToken);
        }

        public ICryptor Cryptor { get; set; }
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }
        public IState PreviousState { get; set; }
    }
}