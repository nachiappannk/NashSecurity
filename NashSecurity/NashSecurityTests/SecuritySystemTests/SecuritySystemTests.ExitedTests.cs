using System;
using NashLink;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.State.Factories.ExitedStateFactory;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.StateBasedTestingTools;
using NashSecurity.Tests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        [TestFixture(typeof(LoggedOutAfterSignInStateFactory))]
        [TestFixture(typeof(LoggedOutAfterSignUpStateFactory))]
        public class ExitedTests : HasExitedData
        {
            private readonly Type _exitedStateFactoryType;

            public ExitedTests(Type exitedStateFactoryType)
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

            [Test, ExpectedException(typeof(SecuritySystem.AccountNameAlreadyTakenException))]
            public void SignUp()
            {
                SecuritySystem.SignUp(AccountInfo);
            }

            [Test]
            public void WhenSignInThenSessionTokenIsValid()
            {
                var sessionToken = SecuritySystem.SignIn(AccountInfo);
                Assert.IsNotNull(sessionToken);
            }

            public const string LongPwd = "";

            [TestCase("WrongPassword1234567890WrongPassword1234567890WrongPasswordkksjflsdkljldka;;asdfjlakkljasdfkljl")]
            [TestCase(LongPwd)]
            [ExpectedException(typeof(SecuritySystem.AccountOrPasswordIsIncorrectException))]
            public void SignInWithWrongPassword(string password)
            {
                var accountInfo = AccountInfo.GetCopy();
                accountInfo.LoginPassword = password;
                SecuritySystem.SignIn(accountInfo);
            }

            [Test, ExpectedException(typeof(SecuritySystem.AccountOrPasswordIsIncorrectException))]
            public void SignInWithWrongUserId()
            {
                SecuritySystem.SignIn(AccountInfo.CorruptUserName());
            }

            [Test, ExpectedException(typeof(SecuritySystem.NotLoggedInException))]
            public void Logout()
            {
                SecuritySystem.Logout(InvalidSessionToken);
            }

            [Test, ExpectedException(typeof(SecuritySystem.NotLoggedInException))]
            public void GetCryptor()
            {
                SecuritySystem.GetCryptor(InvalidSessionToken);
            }
        }
    }
}