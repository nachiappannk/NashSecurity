using System;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.StateBasedTestingTools;
using NashSecurity.Tests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.ScenarioTests
{
    public class HasExitedData : IHasExitedData
    {
        private readonly Type _exitedScenarioFactoryType;
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }
        public ISessionToken InvalidSessionToken { get; set; }
    }
}