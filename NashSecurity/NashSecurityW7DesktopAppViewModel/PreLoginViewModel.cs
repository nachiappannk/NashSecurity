using NashSecurity;

namespace NashSecurityW7DesktopAppViewModel
{
    public class PreLoginViewModel
    {
        private ISecuritySystem _securitySystem;

        public PreLoginViewModel(ISecuritySystem securitySystem, SessionTokenHolder sessionTokenHolder)
        {
            this._securitySystem = securitySystem;
            SignUpViewModel = new SignUpViewModel(securitySystem, sessionTokenHolder);
        }
        public SignUpViewModel SignUpViewModel { get; set; }
    }
}