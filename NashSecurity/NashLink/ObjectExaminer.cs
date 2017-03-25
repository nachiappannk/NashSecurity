using System;
using System.Collections.Generic;
using System.Linq;

namespace NashLink
{
    public static class ObjectExaminer
    {
        private static IEnumerable<string> GetUnSetPropertNamesInternal(this object obj, Type objectType)
        {
            
            foreach (var property in objectType.GetProperties())
                if (property.GetValue(obj, null) == null)
                    yield return property.Name;
        }

        public static IList<string> GetUnSetPropertNames(this object obj, Type objectType = null)
        {
            objectType = objectType ?? obj.GetType();
            return obj.GetUnSetPropertNamesInternal(objectType).ToList();
        }
    }
}