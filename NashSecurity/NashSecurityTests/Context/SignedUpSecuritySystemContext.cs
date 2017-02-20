using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.Context
{
    [Ignore]
    public class SignedUpSecuritySystemContext : SecuritySystemCreatedContext, IEnteredInSecuritySystemContext
    {
        public ISessionToken SessionToken { get; set; }
        
        [SetUp]
        public void SignedUpSecuritySystemContextSetUp()
        {
            GivenSignedUp();
        }

        private void GivenSignedUp()
        {
            TestHelper.TraceMethodName();
            SessionToken = SecuritySystem.SignUp(AccountInfo);
        }
    }
}