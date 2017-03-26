using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashSecurity.Tests.State
{
    public static class StateFactory
    {
        public const string CreatedState = "CreatedState";
        public const string SignedInState = "SignedInState";
        public const string SignedUpState = "SignedUpState";
        public const string AfterSigningInCryptorCreatedState = "AfterSigningInCryptorCreatedState";
        public const string AfterSigningUpCryptorCreatedState = "AfterSigningUpCryptorCreatedState";
        public const string ExitedAfterSigningInState = "ExitedAfterSigningInState";
        public const string ExitedAfterSigningUpState = "ExitedAfterSigningUpState";

        private static readonly Dictionary<string, Func<object>> StateCreatorLookUp 
            = new Dictionary<string, Func<object>>();

        

        static StateFactory()
        {
            var s = StateCreatorLookUp;
            s.Add(CreatedState, () => new CreatedState());
            s.Add(SignedInState, () => new SignedInState());
            s.Add(SignedUpState, () => new SignedUpState());
            s.Add(AfterSigningInCryptorCreatedState, () => new CryptorCreatedState(new SignedInState()));
            s.Add(AfterSigningUpCryptorCreatedState, () => new CryptorCreatedState(new SignedUpState()));
            s.Add(ExitedAfterSigningInState,  () => new ExitedState(new SignedInState()));
            s.Add(ExitedAfterSigningUpState, () => new ExitedState(new SignedUpState()));
        }


        public static object CreateState(string parameter)
        {
            if(!StateCreatorLookUp.ContainsKey(parameter)) 
                throw new Exception("State object could not be created for "+parameter);
            return StateCreatorLookUp[parameter].Invoke();
        }
    }
}
