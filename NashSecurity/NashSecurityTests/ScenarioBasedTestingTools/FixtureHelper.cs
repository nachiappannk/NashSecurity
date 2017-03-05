using System;
using System.Reflection;
using NashSecurity.Tests.ScenarioBasedTestingTools;
using NUnit.Framework;

namespace NashSecurity.Tests.Scenario
{
    [ToDo("This needs to be cleaned up. Dead code needs to be deleted.",
        "Another library needs to be created")]
    public static class FixtureHelper
    {
        public static T CreateTestFixture<T>(Type fixtureType)
        {
            var context = (T)Activator.CreateInstance(fixtureType);
            RunSetUpMethods(context);
            return context;
        }

        private static void RunSetUpMethods(object context)
        {
            RunSetUpMethods(context, context.GetType());
        }

        private static void RunSetUpMethods(object context, Type type)
        {
            if (type != typeof(object))
            {
                RunSetUpMethods(context, type.BaseType);
            }
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                if ((DoesMethodBelongToType(method, type)) && (IsMethodASetUpMethod(method)))
                {
                    method.Invoke(context, new object[] { });
                }
            }
        }

        private static bool IsMethodASetUpMethod(MethodInfo method)
        {
            return (!method.IsAbstract) &&
                   (method.IsPublic) &&
                   (method.ReturnType == typeof(void)) &&
                   (method.GetParameters().Length == 0) &&
                   (method.GetCustomAttribute(typeof(SetUpAttribute)) != null);
        }

        private static bool DoesMethodBelongToType(MethodInfo method, Type type)
        {
            return method.DeclaringType == type;
        }
    }
}