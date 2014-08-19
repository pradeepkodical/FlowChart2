using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace FlowChart2
{
    internal class MethodTracer : IDisposable
    {
        private static object padLock = new object();
        private DateTime dtStartTime;
        private string methodName;
        private string className;
        public MethodTracer()
        {
#if DEBUG
            MethodBase method = new System.Diagnostics.StackFrame(1, false).GetMethod();
            this.methodName = method.Name;
            this.className = method.DeclaringType.Name;
            this.dtStartTime = DateTime.Now;
#endif
        }

        private void LogTrace(string strContents)
        {
#if DEBUG
            try
            {
                Console.Out.WriteLine(strContents);
                return;                
            }
            catch
            {
            }
#endif
        }
        public void Dispose()
        {
#if DEBUG
            TimeSpan timeSpan = DateTime.Now.Subtract(dtStartTime);
            LogTrace(string.Format("{0}.{1}, {2}",
                className,
                methodName,
                timeSpan.TotalMilliseconds
                ));
#endif
        }
    }
}
