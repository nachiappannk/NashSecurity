using System;
using NashSecurity.Tests.SecuritySystemTests;
using NUnit.Framework;

namespace NashSecurity.Tests.Cryptor
{
    public partial class CryptorTests : LoggedInSecuritySystemTests
    {
        protected ICryptor Cryptor { get; set; }
        
        public CryptorTests(Type loginHelperType) : base(loginHelperType)
        {
        }

        [SetUp]
        public void SetUp()
        {
            Cryptor = SecuritySystem.GetCryptor(SessionToken);
        }

        [Test]
        public void EmptyTestToMakeSureThisClassHasTests()
        {
            
        }
    }
}
