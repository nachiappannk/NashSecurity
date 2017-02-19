using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashSecurityW7DesktopAppViewModel;
using NUnit.Framework;
namespace NashSecurityW7DesktopAppViewModel.Tests
{
    [TestFixture()]
    public class ApplicationViewModelTests
    {
        private ApplicationViewModel _applicationViewModel;
        private PreLoginViewModel _preLoginViewModel;

        public void SetUp()
        {
            GivenApplicationIsCreated();
            var mainContentViewModel = _applicationViewModel.MainContentViewModel;
            Assume.That(mainContentViewModel.GetType() == typeof(PreLoginViewModel));
            _preLoginViewModel = mainContentViewModel;

        }

        private void GivenApplicationIsCreated()
        {
            _applicationViewModel = new ApplicationViewModel();
        }

        [Test]
        public void SignUpTests()
        {
            var signUpViewModel = _preLoginViewModel.SignUpViewModel;
            signUpViewModel.UserName = "UserName";
            signUpViewModel.MasterPassword = "MasterPassword";
            signUpViewModel.LoginPassword = "LogingPassword";
            signUpViewModel.SignUpCommand.CanExecute(null);
        }


    }
}
