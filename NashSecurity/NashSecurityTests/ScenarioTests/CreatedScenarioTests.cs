using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.Scenario.Implementation;
using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.ScenarioTests
{
    [TestFixture, Ignore]
    public class CreatedScenarioTests : IHasCreatedData
    {
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }

        [SetUp]
        public void SetUp()
        {
            typeof(CreatedScenario).CopyScenarioDataFromType<IHasCreatedData>(this);
        }
    }
}