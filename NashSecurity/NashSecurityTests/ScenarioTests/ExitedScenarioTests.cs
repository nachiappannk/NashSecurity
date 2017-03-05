using System;
using NashSecurity.Tests.Scenario;
using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.ScenarioTests.Factories.HasExitedData;
using NashSecurity.Tests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.ScenarioTests
{
    [Ignore]
    [TestFixture(typeof(LoggedOutAfterSignInScenarioDataFactory))]
    [TestFixture(typeof(LoggedOutAfterSignUpScenarioDataFactory))]
    public class ExitedScenarioTests : IHasExitedData
    {
        private readonly Type _exitedScenarioFactoryType;
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }
        public ISessionToken InvalidSessionToken { get; set; }


        public ExitedScenarioTests(Type exitedScenarioFactoryType)
        {
            _exitedScenarioFactoryType = exitedScenarioFactoryType;
        }

        [SetUp]
        public void SetUp()
        {
            _exitedScenarioFactoryType.CopyScenarioDataFromFactoryType<IHasExitedData>(this);
        }
    }
}