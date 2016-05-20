using Microsoft.Azure.Devices.Client;
using RockingMicrosoftBand.Common.Extensions;
using RockingMicrosoftBand.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockingMicrosoftBand.IOT.Base
{
    //MWR: You will want to write a webservice to create the conn string for you then save to AppData.  
    //I would even stand up a small web service that could use the current DLL's (.Net) to do device registration send back the key and save it. 
    //You could direct traffic to the site as a filter or just an overlay API. For this demo we are doing a straight pass to the
    //IOT Hub. 
    public static class IoTClient
    {
        //https://github.com/Azure/azure-iot-sdks/blob/master/tools/DeviceExplorer/doc/how_to_use_device_explorer.md
        //Can use to generate device ID and string for here.     
        private const string DeviceConnectionString = "HostName=band2weuioth.azure-devices.net;DeviceId=Band;SharedAccessSignature=SharedAccessSignature sr=band2weuioth.azure-devices.net%2fdevices%2fBand&sig=QfBnYEfN42RISh3EglSYw5z8J4aX7y%2fnw0wVKN3ybcU%3d&se=1454807816;TransportType=Amqp";
        public async static Task SendIOTMessage(this string message, string deviceConnectionString = "")
        {
            deviceConnectionString = string.IsNullOrWhiteSpace(deviceConnectionString) ? DeviceConnectionString : deviceConnectionString;
            DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionString);
            Message eventMessage = new Message(message.ToBytes());
            await deviceClient.SendEventAsync(eventMessage);
        }
       
    }
}
