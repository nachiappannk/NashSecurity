using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.ScenarioBasedTestingTools;

namespace NashSecurity.Tests.ScenarioTests
{
    public class SignedInScenarioDataFactory : IScenarioDataFactory<IHasEnteredData>
    {
        public IHasEnteredData GetScenarioData()
        {
            return new SignedInSecuritySystemScenario();
        }
    }
}