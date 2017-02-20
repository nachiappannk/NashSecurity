using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NashSecurity.Tests.SecuritySystemTests.Support
{
    public static class TestHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void TraceMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            string methodName = stackFrame.GetMethod().Name;
            Debug.Print(methodName);
        }
    }
}