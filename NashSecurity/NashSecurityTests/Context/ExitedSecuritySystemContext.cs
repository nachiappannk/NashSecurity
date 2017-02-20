using System;
using System.Reflection;
using NashSecurity.Tests.SecuritySystemTests.Support;
using NUnit.Framework;

namespace NashSecurity.Tests.Context
{
    [TestFixture(typeof(LoggedOutSecuritySystemContext))]
    [TestFixture(typeof(LoggedOutAfterSiginInSecurityContext))]
    public class ExitedSecuritySystemContext
    {
        private readonly Type _exitedContextType;

        public AccountInfo AccountInfo { get; set; }
        public ISessionToken SessionToken { get; set; }
        public ISecuritySystem SecuritySystem { get; set; }


        public ExitedSecuritySystemContext(Type exitedContextType)
        {
            _exitedContextType = exitedContextType;
        }

        [SetUp]
        public void GivenLoggedOut()
        {
            var context = FixtureHelper.CreateTestFixture<IExitedSecurityContext>(_exitedContextType);
            TestHelper.TraceMethodName();
            AccountInfo = context.AccountInfo;
            SessionToken = context.SessionToken;
            SecuritySystem = context.SecuritySystem;
        }   
    }

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