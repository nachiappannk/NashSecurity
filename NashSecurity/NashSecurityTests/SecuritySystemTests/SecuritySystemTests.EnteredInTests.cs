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
using NashSecurity.Tests.StateBasedTestingTools;
using NashSecurity.Tests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    


    public partial class SecuritySystemTests
    {
        [TestFixture(typeof(SignedInState))]
        [TestFixture(typeof(SignedUpState))]
        public class EnteredTests : HasEnteredStateData
        {
            private readonly Type _stateType;

            public EnteredTests(Type stateType)
            {
                _stateType = stateType;
            }

            [SetUp]
            public void SetUp()
            {
                new NashLinker()
                    .CreateStateFromType(_stateType)
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