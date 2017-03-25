using NashSecurity.Tests.StateAbstractions;

namespace NashSecurity.Tests.State.Factories.EnteredStateFactory
{
    public class SignedInStateFactory
    {
        public IHasEnteredData CreateState()
        {
            return new SignedInState();
        }
    }
}