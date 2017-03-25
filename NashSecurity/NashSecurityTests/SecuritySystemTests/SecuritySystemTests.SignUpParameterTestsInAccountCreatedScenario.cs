using System;
using NashLink;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.State.Factories.EnteredStateFactory;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        [TestFixture(typeof(SignedInStateFactory))]
        [TestFixture(typeof(SignedUpStateFactory))]
        public class SignUpParameterTestsInAccountCreatedScenario : HasEnteredStateData
        {
            private readonly Type _enteredInStateFactoryType;

            public SignUpParameterTestsInAccountCreatedScenario(Type enteredInStateFactoryType)
            {
                _enteredInStateFactoryType = enteredInStateFactoryType;
            }

            [SetUp]
            public void SetUp()
            {
                new NashLinker()
                    .CreateStateWithFactoryType(_enteredInStateFactoryType, "CreateState")
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