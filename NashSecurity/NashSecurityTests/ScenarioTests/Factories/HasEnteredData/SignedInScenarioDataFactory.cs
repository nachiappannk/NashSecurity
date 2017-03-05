using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.Scenario.Implementation;
using NashSecurity.Tests.ScenarioBasedTestingTools;

namespace NashSecurity.Tests.ScenarioTests.Factories.HasEnteredData
{
    public class SignedInScenarioDataFactory : IScenarioDataFactory<IHasEnteredData>
    {
        public IHasEnteredData GetScenarioData()
        {
            return new SignedInScenario();
        }
    }
}