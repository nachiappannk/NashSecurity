using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashSecurity;
using NashSecurity.Tests.Support;

namespace NashSecurityW7DesktopAppViewModel
{
    public class ApplicationViewModel
    {
        private readonly ISecuritySystem _securitySystem;
        public SessionTokenHolder SessionTokenHolder { get; private set; }
        public ApplicationViewModel()
        {
            SessionTokenHolder = new SessionTokenHolder();

            _securitySystem = SecuritySystemFactory.GetSecuritySystem(new MockedAccountDataGateway());
            MainContentViewModel = new PreLoginViewModel(_securitySystem, SessionTokenHolder);
        }
        public PreLoginViewModel MainContentViewModel { get; set; }
    }
}
