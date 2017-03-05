using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.Scenario.Implementation;
using NashSecurity.Tests.ScenarioBasedTestingTools;

namespace NashSecurity.Tests.ScenarioTests
{
    public class LoggedOutAfterSignUpScenarioDataFactory : IScenarioDataFactory<IHasExitedData>
    {
        public IHasExitedData GetScenarioData()
        {
            return new ExitedScenario(new SignedUpScenario());
        }
    }
}