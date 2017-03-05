using System;
using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.ScenarioTests.Factories.HasEnteredData;
using NashSecurity.Tests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.ScenarioTests
{
    [Ignore]
    [TestFixture(typeof(SignedInScenarioDataFactory))]
    [TestFixture(typeof(SignedUpScenarioDataFactory))]
    public class EnteredScenarioTests : IHasEnteredData
    {
        private readonly Type _factoryType;
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public EnteredScenarioTests(Type factoryType)
        {
            _factoryType = factoryType;
            
        }

        [SetUp]
        public virtual void SetUp()
        {
            _factoryType.CopyScenarioDataFromFactoryType<IHasEnteredData>(this);
        }
    }
}