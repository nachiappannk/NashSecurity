using System;

namespace NashLink
{
    public class ObjectCreationIsNotPossibleException : ApplicationException
    {
        public ObjectCreationIsNotPossibleException(Exception e) : base("Unable to create object", e)
        {
            
        }
    }
}