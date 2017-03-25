using System;
using NashLink;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.State.Factories.ExitedStateFactory;
using NashSecurity.Tests.StateBasedTestingTools;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        [ToDo("This is duplicate code. The code is similar to ", typeof(SignUpParameterTestsInAccountCreatedScenario))]
        [TestFixture(typeof(LoggedOutAfterSignInStateFactory))]
        [TestFixture(typeof(LoggedOutAfterSignUpStateFactory))]
        public class SignUpParameterTestsInAccountCreatedScenario1 : HasExitedData
        {
            private readonly Type _exitedStateFactoryType;

            public SignUpParameterTestsInAccountCreatedScenario1(Type exitedStateFactoryType)
            {
                _exitedStateFactoryType = exitedStateFactoryType;
            }

            [SetUp]
            public void SetUp()
            {
                new NashLinker()
                .CreateStateWithFactoryType(_exitedStateFactoryType, "CreateState")
                .EnableFixtureInitializationCheck()
                .EnableStateTrace()
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