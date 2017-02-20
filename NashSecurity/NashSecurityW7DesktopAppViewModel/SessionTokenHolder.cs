using System;
using NashSecurity;

namespace NashSecurityW7DesktopAppViewModel
{
    public class SessionTokenHolder
    {
        private ISessionToken _sessionToken;
        public event Action SessionStateChanged;

        private ISessionToken SessionToken
        {
            get { return _sessionToken; }
            set
            {
                _sessionToken = value;
                FireSessionStateChanged();
            }
        }

        public void SetSessionToken(ISessionToken sessionToken)
        {
            SessionToken = sessionToken;
        }

        public bool IsSessionTokenSet()
        {
            return SessionToken != null;
        }

        public void ResetSessionToken()
        {
            SessionToken = null;
        }

        public ISessionToken GetSessionToken()
        {
            return SessionToken;
        }

        protected virtual void FireSessionStateChanged()
        {
            var handler = SessionStateChanged;
            if (handler != null) handler();
        }
    }
}