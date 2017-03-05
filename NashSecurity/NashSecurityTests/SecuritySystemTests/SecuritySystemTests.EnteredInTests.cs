using System;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        public class EnteredTests : EnteredScenarioTests
        {
            [Test]
            public void LogoutThenNoException()
            {
                SecuritySystem.Logout(SessionToken);
            }

            [Test, ExpectedException(typeof(SecuritySystem.SessionIsInvalidException))]
            public void WhenLoggingOutWithWrongSessionTokenThenException()
            {
                SecuritySystem.Logout(new MockedSessionToken());
            }

            [Test]
            public void GetCryptor()
            {
                var cryptor = SecuritySystem.GetCryptor(SessionToken);
                Assert.NotNull(cryptor);
            }

            [Test, ExpectedException(typeof(SecuritySystem.SessionIsInvalidException))]
            public void GetCryptorWithMockedSession()
            {
                SecuritySystem.GetCryptor(new MockedSessionToken());
            }

            [Test, ExpectedException(typeof(SecuritySystem.AlreadyLoggedInException))]
            public void SignIn()
            {
                SecuritySystem.SignIn(AccountInfo);
            }

            [Test]
            [ExpectedException(typeof(SecuritySystem.AlreadyLoggedInException))]
            public void SignUp()
            {
                SecuritySystem.SignUp(AccountInfo);
            }

            public EnteredTests(Type factoryType)
                : base(factoryType)
            {
            }
        }
    }
}