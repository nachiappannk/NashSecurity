using NashSecurity.AccountData;

namespace NashSecurity
{
    public static class SecuritySystemFactory
    {
        public static ISecuritySystem GetSecuritySystem(IAccountDataGateway accountDataGateway)
        {
            return new SecuritySystem(accountDataGateway);
        }
    }
}