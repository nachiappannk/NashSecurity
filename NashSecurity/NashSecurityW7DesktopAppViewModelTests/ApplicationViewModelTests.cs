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
        protected ApplicationViewModel ApplicationViewModel;
        protected SessionTokenHolder SessionTokenHolder;
        

        [SetUp]
        public void SetUp()
        {
            GivenApplicationIsCreated();
        }

        private void GivenApplicationIsCreated()
        {
            ApplicationViewModel = new ApplicationViewModel();
            SessionTokenHolder = ApplicationViewModel.SessionTokenHolder;
        }

        
    }
}
