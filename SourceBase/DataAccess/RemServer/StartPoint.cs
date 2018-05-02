using System;
using System.Runtime;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization;

using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using Application.BusinessProcess;
using Application.Common;

namespace RemServer
{
    class StartPoint
    {

        [STAThread]
        static void Main(string[] args)
        {
            CLogger.WriteLog(ELogLevel.INFO, "Business Server Activated.");

            string Config = @"RemServer.exe.config";
            RemotingConfiguration.Configure(Config);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(BusinessServerFactory), "BusinessProcess.rem", WellKnownObjectMode.Singleton);
            Console.Write("Business Server Activated. Press Any Key to Disconnect");
            Console.ReadLine();
            CLogger.WriteLog(ELogLevel.INFO, "Business Server Disconnect.");
        }
    }
}

