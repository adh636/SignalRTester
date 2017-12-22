using System;
using System.Reflection;
using CineNet.Common.Logging;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;

namespace SignalRTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetConfiguration.SetUpLogging(Assembly.GetExecutingAssembly());

            Log.Error("Program Running");

            string baseAddress = "http://localhost:9123/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Let's wire up a SignalR client here to easily inspect what
                //  calls are happening
                //
                var hubConnection = new HubConnection(baseAddress);
                IHubProxy eventHubProxy = hubConnection.CreateHubProxy("EventHub");
                eventHubProxy.On<string, ChannelEvent>("OnEvent", (channel, ev) => Log.Error($"Event received on {channel} channel - {@ev}"));
                hubConnection.Start().Wait();

                // Join the channel for task updates in our console window
                //
                eventHubProxy.Invoke("Subscribe", Constants.AdminChannel);
                eventHubProxy.Invoke("Subscribe", Constants.TaskChannel);

                Console.WriteLine($"Server is running on {baseAddress}");
                Console.WriteLine("Press <enter> to stop server");
                Console.ReadLine();
            }
        }
    }
}
