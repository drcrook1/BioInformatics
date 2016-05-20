using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockingMicrosoftBand.DesktopCopout.Base
{
    public class CopoutEventHubHelper
    {
        public EventHubClient Client { get; set; }
        public string ConnectionString { get; set; }
        public CopoutEventHubHelper(string hubName)
        {
            try
            {
                var uri = "sb://band2weusbn.servicebus.windows.net/";
                var accessName = "RootManageSharedAccessKey";
                var keyValue = "uDBIdu2AlHz+G1k0NkxkSJalyXPEzORSiasWAc88ZXU=";

                ConnectionString = "Endpoint=sb://band2weusbn.servicebus.windows.net;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=uDBIdu2AlHz+G1k0NkxkSJalyXPEzORSiasWAc88ZXU=";
                ConnectionString = GetServiceBusConnectionString();
                //string.Format("Endpoint ={0};SharedAccessKeyName={1};SharedAccessKey={2}",
                //uri,accessName, keyValue);
                var nm = NamespaceManager.CreateFromConnectionString(ConnectionString);
                CreateEventHub(hubName, 1, nm);
                Client = EventHubClient.CreateFromConnectionString(ConnectionString, "band2weueh/Partitions/1");
            }
            catch (Exception ex)
            {
            }
        }
        public void CreateEventHub(string eventHubName, int partitions, NamespaceManager nm)
        {
            try
            {
                var ehd = new EventHubDescription(eventHubName);
                ehd.PartitionCount = partitions;
                var hub = nm.CreateEventHubIfNotExists(ehd);
            }
            catch (Exception ex)
            {
            }
        }

        public void SendMessage(string TextToSend)
        {
            try
            {
               // var ac = Client.CreateSender();
               Client.Send(TextToSend.ToEventData());
            }
            catch (Exception ex)
            {
            }
        }
        private  string GetServiceBusConnectionString()
        {
            //string connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            if (string.IsNullOrEmpty(ConnectionString))
            {
                Console.WriteLine("Did not find Service Bus connections string in appsettings (app.config)");
                return string.Empty;
            }
            ServiceBusConnectionStringBuilder builder = new ServiceBusConnectionStringBuilder(ConnectionString);
          //  builder.TransportType = TransportType.Amqp;
            builder.TransportType = TransportType.Amqp;
            return builder.ToString();
        }
    }
}
