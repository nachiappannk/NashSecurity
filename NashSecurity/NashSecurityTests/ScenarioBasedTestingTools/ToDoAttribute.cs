using System;

namespace NashSecurity.Tests.ScenarioBasedTestingTools
{
    [AttributeUsage(AttributeTargets.All,  AllowMultiple = true)]
    public class ToDoAttribute : Attribute
    {
        public ToDoAttribute(params object[] comments)
        {
            
        }
    }
}