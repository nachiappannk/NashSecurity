using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.Scenario.Implementation
{
    public class CreatedScenario : IHasCreatedData
    {
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }


        public CreatedScenario()
        {
            AccountInfo = new AccountInfo("UserName1", "MasterPassword", "LoginPassword");
            GivenSecuritySystemIsCreatedWithMockedDataGateWay();
        }

        private void GivenSecuritySystemIsCreatedWithMockedDataGateWay()
        {
            MockedAccountDataGateway = new MockedAccountDataGateway();
            SecuritySystem = SecuritySystemFactory.GetSecuritySystem(MockedAccountDataGateway);
        }
    }
}