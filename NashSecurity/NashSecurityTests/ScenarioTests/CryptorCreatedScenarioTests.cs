using System;
using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.Scenario.Implementation;
using NashSecurity.Tests.ScenarioBasedTestingTools;
using NUnit.Framework;

namespace NashSecurity.Tests.ScenarioTests
{
    [Ignore]
    public class CryptorCreatedScenarioTests : EnteredScenarioTests, IHasCryptorCreatedData
    {
        private readonly Type _factoryType;
        public ICryptor Cryptor { get; set; }

        public CryptorCreatedScenarioTests(Type factoryType) : base(factoryType)
        {
            _factoryType = factoryType;
        }

        public override  void SetUp()
        {
            var previousScenario = _factoryType.CreateScenarioFromFactoryType<IHasEnteredData>();
            var scenario = new CryptorCreatedScenario(previousScenario);
            scenario.CopyData<IHasCryptorCreatedData>(this);
        }
    }
}