﻿namespace NashSecurity.Tests.SecuritySystemTests.Support
{
    public class SignInLoginHelper : ILoginHelper
    {
        public ISessionToken Login(SecuritySystem securitySystem, MockedAccountDataGateway mockedAccountDataGateway,
            AccountInfo accountInfo)
        {
            var sessionToken = securitySystem.SignUp(accountInfo);
            securitySystem.Logout(sessionToken);
            return securitySystem.SignIn(accountInfo);
        }
    }
}