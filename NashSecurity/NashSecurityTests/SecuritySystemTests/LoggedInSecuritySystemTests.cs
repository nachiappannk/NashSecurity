using System;
using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    [TestFixture(typeof(SignUpLoginHelper))]
    [TestFixture(typeof(SignInLoginHelper))]
    public abstract class LoggedInSecuritySystemTests : SecuritySystemTests
    {
        protected ISessionToken SessionToken { get; set; }
        private ILoginHelper _loginHelper;

        protected LoggedInSecuritySystemTests(Type loginHelperType)
        {
            _loginHelper = (ILoginHelper) Activator.CreateInstance(loginHelperType);
        }

        [SetUp]
        public void GivenLoggedIn()
        {
            TestHelper.TraceMethodName();
            SessionToken = _loginHelper.Login(SecuritySystem, MockedAccountDataGateway, AccountInfo);
        }

        public class LoggedInSecuritySystemSpecificTests : LoggedInSecuritySystemTests
        {
            public LoggedInSecuritySystemSpecificTests(Type loginHelperType)
                : base(loginHelperType)
            {
            }

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
        }
    }
}