using NashSecurity.AccountData;
using NashSecurity.Tests.Context;
using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    [TestFixture()]
    public class SecuritySystemTests : SecuritySystemCreatedContext
    {
        [Test]
        public void WhenSigningUpThenSessionTokenIsGot()
        {
            var sessionToken = SecuritySystem.SignUp(AccountInfo);
            Assert.NotNull(sessionToken);
        }

        [Test, ExpectedException(typeof(SecuritySystem.NotLoggedInException))]
        public void WhenLoggingOutOfUnLoggedInSystemThenException()
        {
            SecuritySystem.Logout(new MockedSessionToken());
        }

        [Test]
        public void WhenSigningUpThenAccountIsCreatedAtAccountDataGateway()
        {
            var created = false;
            EncryptedAccount creationParameter = null;
            MockedAccountDataGateway.StoreEncryptedAccountCalled += (parameter) =>
            {
                created = true;
                creationParameter = parameter;
            };

            SecuritySystem.SignUp(AccountInfo);

            Assert.AreEqual(true, created);
            Assert.NotNull(creationParameter);
            Assert.AreEqual(AccountInfo.AccountName, creationParameter.UserName);
        }

        [Test, ExpectedException(typeof(SecuritySystem.NotLoggedInException))]
        public void WhenLoggingOutOfNotLoggedInSystem()
        {
            SecuritySystem.Logout(new MockedSessionToken());
        }

        [Test, ExpectedException(typeof(SecuritySystem.NotLoggedInException))]
        public void CreateCryptor()
        {
            SecuritySystem.GetCryptor(new MockedSessionToken());
        }
    }
}
