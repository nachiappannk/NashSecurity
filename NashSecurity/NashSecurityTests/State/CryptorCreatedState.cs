using NashLink;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.StateBasedTestingTools;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.State
{
    public class CryptorCreatedState : IHasCryptorCreatedData
    {
        public CryptorCreatedState(IHasEnteredData enteredData)
        {
            enteredData.CopyPossibleProperties(this);
            Cryptor = SecuritySystem.GetCryptor(SessionToken);
        }

        public ICryptor Cryptor { get; set; }
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }
    }

    public class CryptorCreatedAfterSigningInStateFactory
    {
        public CryptorCreatedState CreateState()
        {
            return new CryptorCreatedState(new SignedInState());
        }
    }

    public class CryptorCreatedAfterSigningUpStateFactory
    {
        public CryptorCreatedState CreateState()
        {
            return new CryptorCreatedState(new SignedUpState());
        }
    }
}