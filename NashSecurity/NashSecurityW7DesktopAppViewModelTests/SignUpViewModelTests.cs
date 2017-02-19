using System;
using NUnit.Framework;

namespace NashSecurityW7DesktopAppViewModel.Tests
{
    public class SignUpViewModelTests : PreLoginViewModelTests
    {
        private SignUpViewModel SignUpViewModel;

        [SetUp]
        public void SignUpSetUp()
        {
            SignUpViewModel = PreLoginViewModel.SignUpViewModel;
        }

        [TestCase("user", "1234567890123456", "12345678", false)]
        [TestCase("aValidUserName", "small", "12345678", false)]
        [TestCase("aValidUserName", "1234567890123456", "small", false)]
        [TestCase(null, "1234567890123456", "12345678", false)]
        [TestCase("aValidUserName", null, "12345678", false)]
        [TestCase("aValidUserName", "1234567890123456", null, false)]
        public void When_sign_up_parameters_are_set_then_sign_up_possibility_is_determined(string username,
            string masterPassword, string loginPassword, bool isSignUpPossible)
        {
            SignUpViewModel.UserName = username;
            SignUpViewModel.MasterPassword = masterPassword;
            SignUpViewModel.LoginPassword = loginPassword;
            Assert.AreEqual(isSignUpPossible, SignUpViewModel.SignUpCommand.CanExecute(null));
        }

        [Test]
        public void When_sign_up_parameter_is_set_then_changed_event_is_raised()
        {
            AssertChangedEventOnSettingParameter(() => SignUpViewModel.UserName = "something");
            AssertChangedEventOnSettingParameter(() => SignUpViewModel.LoginPassword = "something");
            AssertChangedEventOnSettingParameter(() => SignUpViewModel.MasterPassword = "something");
        }

        [Test]
        public void When_signed_up_then_session_token_is_set()
        {
            
        }

        private void AssertChangedEventOnSettingParameter(Action parameterSettingAction)
        {
            var canExecutedChanged = false;
            SignUpViewModel.SignUpCommand.CanExecuteChanged += (sender, args) => { canExecutedChanged = true; };
            parameterSettingAction.Invoke();
            Assert.True(canExecutedChanged);
        }
    }
}