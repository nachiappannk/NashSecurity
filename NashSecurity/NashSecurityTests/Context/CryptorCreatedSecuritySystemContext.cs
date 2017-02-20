using NashSecurity.Tests.SecuritySystemTests.Support;

namespace NashSecurity.Tests.Context
{
    public class CryptorCreatedSecuritySystemContext : IEnteredInSecuritySystemContext
    {
        public CryptorCreatedSecuritySystemContext(IEnteredInSecuritySystemContext innerContext)
        {
            SecuritySystem = innerContext.SecuritySystem;
            SessionToken = innerContext.SessionToken;
            MockedAccountDataGateway = innerContext.MockedAccountDataGateway;
            AccountInfo = innerContext.AccountInfo;

            GivenCryptorIsCreated();
        }

        private void GivenCryptorIsCreated()
        {
            TestHelper.TraceMethodName();
            Cryptor = SecuritySystem.GetCryptor(SessionToken);
        }

        public ICryptor Cryptor { get; set; }
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }
    }
}