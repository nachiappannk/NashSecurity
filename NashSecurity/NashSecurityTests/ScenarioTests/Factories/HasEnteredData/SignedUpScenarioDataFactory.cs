using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.ScenarioBasedTestingTools;

namespace NashSecurity.Tests.ScenarioTests
{
    public class SignedUpScenarioDataFactory : IScenarioDataFactory<IHasEnteredData>
    {
        public IHasEnteredData GetScenarioData()
        {
            return new SignedUpScenario();
        }
    }
}