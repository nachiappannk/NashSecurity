using System;
using NashSecurity.Tests.ScenarioBasedTestingTools;
using NashSecurity.Tests.ScenarioTests;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        [ToDo("This is duplicate code. The code is similar to ", typeof(SignUpParameterTestsInAccountCreatedScenario))]
        public class SignUpParameterTestsInAccountCreatedScenario1 : ExitedScenarioTests
        {
            public SignUpParameterTestsInAccountCreatedScenario1(Type enteredInContextType)
                : base(enteredInContextType)
            {
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