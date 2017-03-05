using NashSecurity.Tests.Scenario.Implementation;
using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.Scenario
{
    public class SignedInSecuritySystemScenario : IHasEnteredData
    {
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public SignedInSecuritySystemScenario()
        {
            var previousScenario = new ExitedScenario(new SignedUpScenario());
            previousScenario.MoveToScenario<IHasExitedData, IHasEnteredData>(this);
            SessionToken = SecuritySystem.SignIn(AccountInfo);
        }
    }
}