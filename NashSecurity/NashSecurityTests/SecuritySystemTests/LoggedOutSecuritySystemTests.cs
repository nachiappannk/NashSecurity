﻿using System;
using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    [TestFixture(typeof(SignUpLoginHelper))]
    [TestFixture(typeof(SignInLoginHelper))]
    public class LoggedOutSecuritySystemTests : LoggedInSecuritySystemTests
    {
        public LoggedOutSecuritySystemTests(Type loginHelperType) : base(loginHelperType)
        {
        }

        [SetUp]
        public void GivenLoggedOut()
        {
            TestHelper.TraceMethodName();
            SecuritySystem.Logout(SessionToken);
        }

        public class SpecificTests : LoggedOutSecuritySystemTests
        {
            public SpecificTests(Type loginHelperType) : base(loginHelperType)
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
                SecuritySystem.Logout(SessionToken);
            }

            [Test, ExpectedException(typeof(SecuritySystem.NotLoggedInException))]
            public void GetCryptor()
            {
                SecuritySystem.GetCryptor(SessionToken);
            }
        }
    }
}