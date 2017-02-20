using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.Context
{
    [TestFixture]
    public abstract class SecuritySystemCreatedContext
    {
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }

        [SetUp]
        public void SecuritySystemCreatedContextSetUp()
        {
            AccountInfo = new AccountInfo("UserName1", "MasterPassword", "LoginPassword");
            GivenSecuritySystemIsCreatedWithMockedDataGateWay();
        }

        private void GivenSecuritySystemIsCreatedWithMockedDataGateWay()
        {
            TestHelper.TraceMethodName();
            MockedAccountDataGateway = new MockedAccountDataGateway();
            SecuritySystem = SecuritySystemFactory.GetSecuritySystem(MockedAccountDataGateway);
        }
    }
}