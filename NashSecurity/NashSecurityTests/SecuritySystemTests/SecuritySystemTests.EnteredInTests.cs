using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AutoMapper;
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
        [TestFixture(StateFactory.SignedInState)]
        [TestFixture(StateFactory.SignedUpState)]
        public class EnteredTests 
            : IHasSessionToken, IHasSecuritySystem, IHasMockedAccountDataGateway, IHasAccountInfo
        {
            private readonly string _stateCreationParam;

            public ISessionToken SessionToken { get; set; }
            public ISecuritySystem SecuritySystem { get; set; }
            public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
            public AccountInfo AccountInfo { get; set; }

            public EnteredTests(string stateCreationParam)
            {
                _stateCreationParam = stateCreationParam;
            }

            [SetUp]
            public void SetUp()
            {
                new NashLinker()
                    .UseState(StateFactory.CreateState(_stateCreationParam))
                    .EnableStateTrace("PreviousState")
                    .EnableFixtureInitializationCheck()
                    .LinkStateToFixture(this);
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