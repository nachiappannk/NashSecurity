using NashSecurity.Tests.StateAbstractions;

namespace NashSecurity.Tests.State.Factories.EnteredStateFactory
{
    public class SignedUpStateFactory
    {
        public IHasEnteredData CreateState()
        {
            return new SignedUpState();
        }
    }
}