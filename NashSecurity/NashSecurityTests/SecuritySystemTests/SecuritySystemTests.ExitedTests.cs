using System;
using NashLink;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.State;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        [TestFixture(StateFactory.ExitedAfterSigningInState)]
        [TestFixture(StateFactory.ExitedAfterSigningUpState)]
        public class ExitedTests 
            : IHasMockedAccountDataGateway, IHasSecuritySystem, IHasAccountInfo, IHasInvalidSessionToken
        {
            public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
            public ISecuritySystem SecuritySystem { get; set; }
            public AccountInfo AccountInfo { get; set; }
            public ISessionToken InvalidSessionToken { get; set; }

            private readonly string _stateParams;

            public ExitedTests(string stateParams)
            {
                _stateParams = stateParams;
            }

            [SetUp]
            public void SetUp()
            {
                new NashLinker()
                .UseState(StateFactory.CreateState(_stateParams))
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