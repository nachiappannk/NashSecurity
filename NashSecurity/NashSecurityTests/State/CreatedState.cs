using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;

namespace NashSecurity.Tests.State
{
    public class CreatedState : IHasCreatedData, IState
    {
        public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public CreatedState()
        {
            AccountInfo = new AccountInfo("UserName1", "MasterPassword", "LoginPassword");
            MockedAccountDataGateway = new MockedAccountDataGateway();
            SecuritySystem = SecuritySystemFactory.GetSecuritySystem(MockedAccountDataGateway);
        }
    }
}