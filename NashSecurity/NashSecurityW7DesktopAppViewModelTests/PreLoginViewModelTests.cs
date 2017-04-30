using NUnit.Framework;

namespace NashSecurityW7DesktopAppViewModel.Tests
{
    public class PreLoginViewModelTests : ApplicationViewModelTests
    {
        protected PreLoginViewModel PreLoginViewModel;
        [SetUp]
        public void PreLoginSetUp()
        {
            GivenMainContentIsPreLogin();
        }

        private void GivenMainContentIsPreLogin()
        {
            var mainContentViewModel = ApplicationViewModel.MainContentViewModel;
            PreLoginViewModel = mainContentViewModel;
            Assume.That(mainContentViewModel.GetType() == typeof(PreLoginViewModel));
        }
    }
}