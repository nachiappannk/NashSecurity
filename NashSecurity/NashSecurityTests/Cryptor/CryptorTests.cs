﻿using System;
using System.CodeDom;
using NashLink;
using NashSecurity.InternalCryptor;
using NashSecurity.Tests.ScenarioTests;
using NashSecurity.Tests.SecuritySystemTests;
using NashSecurity.Tests.State;
using NashSecurity.Tests.StateAbstractions;
using NashSecurity.Tests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.Cryptor
{

    public partial class CryptorTests
    {
        [TestFixture(StateFactory.AfterSigningInCryptorCreatedState)]
        [TestFixture(StateFactory.AfterSigningUpCryptorCreatedState)]
        public class CryptorCreatedState : IHasCryptorCreatedData
        {
            private readonly string _stateParam;
            public ISessionToken SessionToken { get; set; }
            public ISecuritySystem SecuritySystem { get; set; }
            public MockedAccountDataGateway MockedAccountDataGateway { get; set; }
            public AccountInfo AccountInfo { get; set; }
            public ICryptor Cryptor { get; set; }


            public CryptorCreatedState(string stateParam)
            {
                _stateParam = stateParam;
            }

            [SetUp]
            public void SetUp()
            {
                new NashLinker()
                .UseState(StateFactory.CreateState(_stateParam))
                .EnableFixtureInitializationCheck()
                .EnableStateTrace()
                .LinkStateToFixture(this);
            }


            [Test]
            public void Encrypt()
            {
                byte[] inputBytes = new byte[] { 00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 1, 16, 17, 18, 19, 20 };
                byte[] expectedEncryptedBytes = InternalContentCryptor.EncryptBytes(AccountInfo.MasterPassword, inputBytes);
                var encryptedBytes = Cryptor.Encrypt(SessionToken, inputBytes);
                CollectionAssert.AreEqual(expectedEncryptedBytes, encryptedBytes);
            }

            [Test]
            public void Decrypt()
            {
                byte[] plainBytes = new byte[] { 00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 1, 16, 17, 18, 19, 20 };
                byte[] encryptedBytes = InternalContentCryptor.EncryptBytes(AccountInfo.MasterPassword, plainBytes);

                byte[] expectedDecryptedBytes = InternalContentCryptor.DecryptBytes(AccountInfo.MasterPassword, encryptedBytes);
                var decryptedBytes = Cryptor.Decrypt(SessionToken, encryptedBytes);
                CollectionAssert.AreEqual(expectedDecryptedBytes, decryptedBytes);
            }


        }
    }
}
