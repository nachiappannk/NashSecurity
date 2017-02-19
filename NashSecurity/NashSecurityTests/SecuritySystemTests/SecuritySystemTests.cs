using System;
using NashSecurity.AccountData;
using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    [TestFixture()]
    public class SecuritySystemTests
    {
        protected MockedAccountDataGateway MockedAccountDataGateway;
        protected ISecuritySystem SecuritySystem { get; set; }
        protected readonly AccountInfo AccountInfo = new AccountInfo("UserName1", "MasterPassword", "LoginPassword");

        [SetUp]
        public void GivenSecuritySystemIsCreatedWithMockedDataGateWay()
        {
            TestHelper.TraceMethodName();
            MockedAccountDataGateway = new MockedAccountDataGateway();
            SecuritySystem = SecuritySystemFactory.GetSecuritySystem(MockedAccountDataGateway);
        }

        public class SecuritySystemSpecificTests : SecuritySystemTests
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
}
