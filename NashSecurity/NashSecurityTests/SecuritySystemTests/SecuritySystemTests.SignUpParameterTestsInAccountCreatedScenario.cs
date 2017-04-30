using System;
using NashLink;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.State;
using NashSecurity.Tests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        [TestFixture(StateFactory.SignedInState)]
        [TestFixture(StateFactory.SignedUpState)]
        [TestFixture(StateFactory.ExitedAfterSigningInState)]
        [TestFixture(StateFactory.ExitedAfterSigningUpState)]
        public class SignUpParameterTestsInAccountCreatedScenario
        {
            public ISecuritySystem SecuritySystem { get; set; }
            public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
            public AccountInfo AccountInfo { get; set; }

            private readonly string _stateParams;

            public SignUpParameterTestsInAccountCreatedScenario(string stateParams)
            {
                _stateParams = stateParams;
            }

            [SetUp]
            public void SetUp()
            {
                new NashLinker()
                    .UseState(StateFactory.CreateState(_stateParams))
                    .EnableStateTrace()
                    .EnableFixtureInitializationCheck()
                    .LinkStateToFixture(this);
            }

            [Test]
            public void AlreadyExistingUserNameForSignUp()
            {
                Assert.Throws(typeof(SecuritySystem.AccountNameAlreadyTakenException),
                    () => SecuritySystem.AssertUserNameIsValidForSignUp(AccountInfo.AccountName));
            }
        }
    }
}