using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.Scenario.Implementation
{
    public class SignedUpScenario : IHasEnteredData
    {
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public SignedUpScenario()
        {
            var previousScenario = new CreatedScenario();
            previousScenario.MoveToScenario<IHasCreatedData, IHasEnteredData>(this);
            SessionToken = SecuritySystem.SignUp(AccountInfo);
        }
    }
}