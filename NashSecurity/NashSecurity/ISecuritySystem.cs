namespace NashSecurity
{
    public interface ISecuritySystem
    {
        ISessionToken SignUp(string userName, string masterPassword, string loginPassword);
        ISessionToken SignIn(string userName, string loginPassword);
        void Logout(ISessionToken sessionToken);
        ICryptor GetCryptor(ISessionToken sessionToken);
        void AssertUserNameIsValidForSignUp(string userName);
        void AssertMasterPasswordIsValidForSignUp(string masterPassword);
        void AssertLoginPasswordIsValidForSignUp(string logiPassword);
    }
}