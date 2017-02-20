using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.Context
{
    [Ignore]
    public class SignedInSecuritySystemContext : LoggedOutSecuritySystemContext, IEnteredInSecuritySystemContext
    {
        [SetUp]
        public void SignedInSecuritySystemContextSetUp()
        {
            GivenSignedIn();
        }

        private void GivenSignedIn()
        {
            TestHelper.TraceMethodName();
            SessionToken = SecuritySystem.SignIn(AccountInfo);
        }
    }
}