using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.SecuritySystemTests.Support;

namespace NashSecurity.Tests.Scenario
{
    public class CryptorCreatedScenario : IHasCryptorCreatedData
    {
        public CryptorCreatedScenario(IHasEnteredData enteredData)
        {
            enteredData.MoveToScenario<IHasEnteredData, IHasCryptorCreatedData>(this);

            GivenCryptorIsCreated();
        }

        private void GivenCryptorIsCreated()
        {
            Cryptor = SecuritySystem.GetCryptor(SessionToken);
        }

        public ICryptor Cryptor { get; set; }
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }
    }
}