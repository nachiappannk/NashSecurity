using System;
using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.Context
{
    [TestFixture(typeof(SignedInSecuritySystemContext))]
    [TestFixture(typeof(SignedUpSecuritySystemContext))]
    public class EnteredInSecuritySystemContext
    {
        private readonly Type _enteredInContextType;

        public ISecuritySystem SecuritySystem { get; set; }
        public ISessionToken SessionToken { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public EnteredInSecuritySystemContext(Type enteredInContextType)
        {
            _enteredInContextType = enteredInContextType;
        }

        [SetUp]
        public void SetUp()
        {
            var context = FixtureHelper.CreateTestFixture<IEnteredInSecuritySystemContext>(_enteredInContextType);
            SecuritySystem = context.SecuritySystem;
            SessionToken = context.SessionToken;
            AccountInfo = context.AccountInfo;
        }
    }
}