using System;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        public class ExitedTests : ExitedScenarioTests
        {
            public ExitedTests(Type exitedContextType)
                : base(exitedContextType)
            {
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