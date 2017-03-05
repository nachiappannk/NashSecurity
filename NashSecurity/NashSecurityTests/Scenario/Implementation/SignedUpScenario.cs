using NashSecurity.Tests.Scenario.Implementation;
using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.SecuritySystemTests.Support;

namespace NashSecurity.Tests.Scenario
{
    public class SignedUpScenario : IHasEnteredData
    {
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public SignedUpScenario()
        {
            var previousScenario = new SecuritySystemCreatedScenario();
            previousScenario.MoveToScenario<IHasCreatedData, IHasEnteredData>(this);
            SessionToken = SecuritySystem.SignUp(AccountInfo);
        }
    }
}