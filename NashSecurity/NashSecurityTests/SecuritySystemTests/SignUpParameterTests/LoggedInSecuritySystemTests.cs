using System;
using NashSecurity.Tests.Context;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests.SignUpParameterTests
{
    [TestFixture]
    public class SignUpParametersInLoggedInSystemsTests : EnteredInSecuritySystemContext
    {
        public SignUpParametersInLoggedInSystemsTests(Type enteredInContextType)
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