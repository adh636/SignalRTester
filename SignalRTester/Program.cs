using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CineNet.Common.Logging;

namespace SignalRTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetConfiguration.SetUpLogging(Assembly.GetExecutingAssembly());

            Log.Error("Program Running");
        }
    }
}
