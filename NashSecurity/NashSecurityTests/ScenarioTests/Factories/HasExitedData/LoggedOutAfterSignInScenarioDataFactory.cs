using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.Scenario.Implementation;
using NashSecurity.Tests.ScenarioBasedTestingTools;

namespace NashSecurity.Tests.ScenarioTests.Factories.HasExitedData
{
    public class LoggedOutAfterSignInScenarioDataFactory : IScenarioDataFactory<IHasExitedData>
    {
        public IHasExitedData GetScenarioData()
        {
            return new ExitedScenario(new SignedInScenario());
        }
    }
}