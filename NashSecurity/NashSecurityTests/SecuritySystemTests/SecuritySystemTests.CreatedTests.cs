using System;
using NashSecurity.AccountData;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.State;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;
using NUnit.Framework;
using NashLink;

namespace NashSecurity.Tests.SecuritySystemTests
{
    public partial class SecuritySystemTests
    {
        [TestFixture()]
        public class CreatedTests : HasCreatedStateData
        {
            [SetUp]
            public void SetUp()
            {
                new NashLinker()
                    .UseState(StateFactory.CreateState(StateFactory.CreatedState))
                    .EnableFixtureInitializationCheck()
                    .EnableStateTrace("PreviousState")
                    .LinkStateToFixture(this);
            }

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