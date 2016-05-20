using Amqp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockingMicrosoftBand.EventHub.Base
{
    public class AMQPSender
    {
        public int Port { get { return 5671; } }
        public string HubName { get; set; }
        public string ServiceBusAddress
        {
            //band2iot2sa2powerbi.azure-devices.net
            //get { return string.Format("{0}.servicebus.windows.net", HubName); }
            get { return string.Format("{0}.azure-devices.net", HubName); }
        }
        public string SharedAccessKeyName { get; set; }
        public string SharedAccessKeyPassword { get; set; }
        public Address Address { get; set; }
        public AMQPSender()
        {
             HubName = "band2iot2sa2powerbi";
            // SharedAccessKeyName = "device";
            //SharedAccessKeyPassword = "2Jp+NfkSvz6tUZgn5Ka22srKuZpcW0bAFTjNZ+bX5hI=";
            SharedAccessKeyPassword = "eu/IV+uAjfW4dDd6WxoxHt+Ce5D7c4wLhr0qmVhZHss=";
            Address = new Address(ServiceBusAddress, Port, "alexismachine", SharedAccessKeyPassword);
        }
      
       
        public async Task SendToServiceBusViaAMQP(string payload)
        {
            try
            {
                var eh = "HostName=band2weuioth.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=s3VUAuna0MofpeFltzvDFHsB2pPXfa7nxPIW6H+lbdM=;TransportType=Aqmp";
                Amqp.Connection connection = new Amqp.Connection(new Address(eh));


                Amqp.Session session = new Amqp.Session(connection);
                Amqp.SenderLink senderLink = new Amqp.SenderLink(session,
                    string.Format("send-link:{0}", HubName), HubName);

                Amqp.Message message = new Amqp.Message()
                {
                    BodySection = new Amqp.Framing.Data()
                    {
                        Binary = System.Text.Encoding.UTF8.GetBytes(payload)
                    }
                };

                message.MessageAnnotations = new Amqp.Framing.MessageAnnotations();
                message.MessageAnnotations[new Amqp.Types.Symbol("x-opt-partition-key")] =
                   string.Format("pk:", HubName);

                await senderLink.SendAsync(message);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
