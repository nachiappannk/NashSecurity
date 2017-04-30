using System;

namespace NashLink
{
    public class StateCreatorNotConfiguredException : ApplicationException
    {
        public StateCreatorNotConfiguredException() : 
            base("CreateStateWithType or CreateStateWithFactoryType has to be called")
        {
            
        }
    }
}