using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.Scenario.Implementation
{
    public class SignedInScenario : IHasEnteredData
    {
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public SignedInScenario()
        {
            var previousScenario = new ExitedScenario(new SignedUpScenario());
            previousScenario.MoveToScenario<IHasExitedData, IHasEnteredData>(this);
            SessionToken = SecuritySystem.SignIn(AccountInfo);
        }
    }
}