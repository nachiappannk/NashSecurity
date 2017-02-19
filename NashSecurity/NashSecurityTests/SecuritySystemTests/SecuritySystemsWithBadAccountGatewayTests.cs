using NUnit.Framework;

namespace NashSecurity.Tests.SecuritySystemTests
{
    [TestFixture]
    public class SecuritySystemsWithBadAccountGatewayTests
    {
        private SecuritySystem _securitySystem;

        public void SetUp()
        {
            _securitySystem = new SecuritySystem(null);
        }
    }
}