using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.SecuritySystemTests.Support;

namespace NashSecurity.Tests.Scenario.Implementation
{
    public class ExitedScenario : IHasExitedData
    {
        public ISessionToken InvalidSessionToken { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public ExitedScenario(IHasEnteredData enteredScenario)
        {
            var previousScenario = enteredScenario;
            previousScenario.MoveToScenario<IHasEnteredData, IHasExitedData>(this);
            SecuritySystem.Logout(previousScenario.SessionToken);
            InvalidSessionToken = previousScenario.SessionToken;
        }
    }
}