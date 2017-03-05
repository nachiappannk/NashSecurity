using System;
using NashSecurity.Tests.ScenarioTests;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        public class SignUpParameterTestsInAccountCreatedScenario : EnteredScenarioTests
        {
            public SignUpParameterTestsInAccountCreatedScenario(Type enteredInContextType)
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