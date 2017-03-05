using System;
using NashSecurity.Tests.ScenarioTests;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        public class SignUpParametersTestsInExitedScenario : ExitedScenarioTests
        {
            [TestCase(null, typeof(SecuritySystem.UserNameForSignUpIsEmptyException))]
            [TestCase(" ", typeof(SecuritySystem.UserNameForSignUpIsEmptyException))]
            [TestCase("a", typeof(SecuritySystem.UserNameForSignUpIsTooShortException))]
            [TestCase("1234567", typeof(SecuritySystem.UserNameForSignUpIsTooShortException))]
            [TestCase("123 5678", typeof(SecuritySystem.UserNameForSignUpHasCharactersNotAllowedException))]
            [TestCase("123@5678", typeof(SecuritySystem.UserNameForSignUpHasCharactersNotAllowedException))]
            public void WrongUserNameForSignUpValidationTests(string userName, Type expectedExceptionType)
            {
                Assert.Throws(expectedExceptionType,
                    () => AssertUserNameForSignUpIsCorrect(userName));
            }

            [TestCase("12345678")]
            [TestCase("abc-dfefe")]
            [TestCase("abc_dfefe")]
            [TestCase("_bc_dfefe")]
            [TestCase("abc_dfef___")]
            [TestCase("abc_dfef_-_")]
            public void CorrectUserNameForSignUpValidationTests(string userName)
            {
                AssertUserNameForSignUpIsCorrect(userName);
            }

            [TestCase("Te", typeof(SecuritySystem.MasterPasswordIsShortException))]
            [TestCase("123456789012345", typeof(SecuritySystem.MasterPasswordIsShortException))]
            [TestCase(null, typeof(SecuritySystem.MasterPasswordIsShortException))]
            public void WrongMasterPasswordForSignUpValidationTests(string masterPassword, Type expectedExceptionType)
            {
                Assert.Throws(expectedExceptionType,
                    () => SecuritySystem.AssertMasterPasswordIsValidForSignUp(masterPassword));
            }

            [TestCase("1234567890123456")]
            public void CorrectMasterPasswordForSignUpValidationTests(string masterPassword)
            {
                SecuritySystem.AssertMasterPasswordIsValidForSignUp(masterPassword);
            }

            [TestCase("Te", typeof(SecuritySystem.LoginPasswordIsShortException))]
            [TestCase("1234567", typeof(SecuritySystem.LoginPasswordIsShortException))]
            [TestCase(null, typeof(SecuritySystem.LoginPasswordIsShortException))]
            public void WrongLoginPasswordForSignUpValidationTests(string logiPassword, Type expectedExceptionType)
            {
                Assert.Throws(expectedExceptionType,
                    () => SecuritySystem.AssertLoginPasswordIsValidForSignUp(logiPassword));
            }

            [TestCase("12345678")]
            public void CorrectLoginPasswordForSignUpValidationTests(string loginPassword)
            {
                SecuritySystem.AssertLoginPasswordIsValidForSignUp(loginPassword);
            }

            private void AssertUserNameForSignUpIsCorrect(string userName)
            {
                SecuritySystem.AssertUserNameIsValidForSignUp(userName);
            }

            public SignUpParametersTestsInExitedScenario(Type exitedScenarioFactoryType)
                : base(exitedScenarioFactoryType)
            {
            }
        }
    }
}