namespace NashSecurity
{
    public class SessionDataHolder
    {
        private SessionToken _sessionToken;
        private SessionData _sessionData;

        public ISessionToken CreateSession(string accountName, string masterPassword)
        {
            AssertNotLoggedIn();
            _sessionToken = new SessionToken();
            _sessionData = new SessionData()
            {
                MasterPassword = masterPassword,
            };
            return _sessionToken;
        }

        private bool IsSessionCreated()
        {
            return _sessionToken != null;
        }

        private void AssertLoggedIn()
        {
            if (!IsSessionCreated()) throw new SecuritySystem.NotLoggedInException();
        }

        public void AssertNotLoggedIn()
        {
            if (IsSessionCreated()) throw new SecuritySystem.AlreadyLoggedInException();
        }

        public void AssertSessionTokenIsValid(ISessionToken sessionToken)
        {
            if (!IsSessionTokenValid(sessionToken)) throw new SecuritySystem.SessionIsInvalidException();
        }

        public void AssetCallIsFromCorrectClient(ISessionToken sessionToken)
        {
            AssertLoggedIn();
            AssertSessionTokenIsValid(sessionToken);
        }

        public string GetMasterPassword(ISessionToken sessionToken)
        {
            AssetCallIsFromCorrectClient(sessionToken);
            return _sessionData.MasterPassword;
        }

        public void DeleteSession(ISessionToken sessionToken)
        {
            AssetCallIsFromCorrectClient(sessionToken);
            _sessionToken = null;
            _sessionData = null;
        }

        public bool IsSessionTokenValid(ISessionToken sessionToken)
        {
            return (sessionToken.Equals(_sessionToken));
        }

        public class SessionToken : ISessionToken
        {
        }
    }
}