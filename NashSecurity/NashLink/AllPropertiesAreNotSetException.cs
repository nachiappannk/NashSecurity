using System;
using System.Collections.Generic;

namespace NashLink
{
    public class AllPropertiesAreNotSetException : ApplicationException
    {
        public IEnumerable<string> UnSetPropertyNames { get; private set; }

        public AllPropertiesAreNotSetException(IEnumerable<string> unSetPropertyNames)
        {
            this.UnSetPropertyNames = unSetPropertyNames;
        }

        public override string ToString()
        {
            return this.GetType().Name + " The unset properties are "+ string.Join(",", UnSetPropertyNames);
        }
    }
}