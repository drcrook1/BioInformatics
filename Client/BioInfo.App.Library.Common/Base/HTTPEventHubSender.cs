using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.App.Library.Common.Base
{
    public static class HTTPEventHubSender
    {
        //TODO ADD NEW HTTPCLIENT WRAPPER rewraps HttpClient into easy to use and hand

        public static async Task<HttpResponseMessage> PostCSVAsync(string serviceNamespace, string deviceName,string anyreading)
        {
            //var sas = "SharedAccessSignature sr=https%3a%2f%2freddogeventhub.servicebus.windows.net%2ftemperature%2fpublishers%2flivingroom%2fmessages&sig=I7n%2bqlIExBRs23V4mcYYfYVYhc6adOlMAeTY9VM9kNg%3d&se=1405562228&skn=SenderDevice";

            //// Namespace info.
            //band2weuioth
            var sas = "SharedAccessSignature sr=band2weuioth.azure-devices.net&sig=PqwMCuUXturMYsgdj1%2fz7D7diw1Vy2yQ%2bVVgjfvgpMA%3d&se=1485491273&skn=iothubowner;TransportType=Aqmp;";//, serviceNamespace, deviceName);
            //var deviceAddress = string.Format("HostName=band2weuioth.azure-devices.net;DeviceId=Band;SharedAccessKey=aMgxJWSrQIqP3NmWyxguMWgWA+7Pnql65jzDlP0ObEY=;TransportType=Aqmp;", serviceNamespace, deviceName);
            try
            {
                //vBaseAddress = new Uri(string.Format("https://band2weuioth.azure-devices.net", serviceNamespace)) };
                var httpClient = new HttpClient {
                    // BaseAddress = new Uri("HostName=band2weuioth.azure-devices.net;DeviceId=Band;SharedAccessKey=aMgxJWSrQIqP3NmWyxguMWgWA+7Pnql65jzDlP0ObEY=;TransportType=Aqmp;")
                    BaseAddress = new Uri(string.Format("https://band2weuioth.azure-devices.net", serviceNamespace))
                };

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", sas);
                var content = new StringContent(anyreading, Encoding.UTF8, "text/plain");

                //   var a =  await httpClient.PostAsync(string.Format("/devices/{0}/messages/events", deviceName), content);
                // var b = await httpClient.PostAsync(string.Format("/devices/{0}/", deviceName), content);
                //("
                var a = await httpClient.PostAsync(string.Format("/devices/{0}/messages/events", "Band"), content);
               // var b = await httpClient.PostAsync(string.Format("publishers/{1}/messages", "band2weuioth", deviceName), content);
                return a;
       
            }
            catch (Exception ex)
            {

            }
            return null;
            // }
        }
        //private static Task<HttpResponseMessage> PostMessage(string text)
        //{
        //    // Use Event Hubs Signature Generator 0.2.0.1 to generate the token
        //    // https://github.com/sandrinodimattia/RedDog/releases/tag/0.2.0.1
        //    // http://fabriccontroller.net/blog/posts/iot-with-azure-service-bus-event-hubs-authenticating-and-sending-from-any-type-of-device-net-and-js-samples/
        //    var sas = "SharedAccessSignature sr=band2weuioth.azure-devices.net&sig=PqwMCuUXturMYsgdj1%2fz7D7diw1Vy2yQ%2bVVgjfvgpMA%3d&se=1485491273&skn=iothubowner;TransportType=Aqmp;";

        //    // Namespace info.
        //    var serviceNamespace = "YOUR NAMESPACE";
        //    var hubName = "YOUR HUB NAME";
        //    var url = string.Format("{0}/publishers/{1}/messages", hubName, "Band");

        //    // Create client.
        //    var httpClient = new HttpClient
        //    {
        //        BaseAddress = new Uri(string.Format("https://{0}.servicebus.windows.net/", serviceNamespace))
        //    };


        //    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", sas);

        //    var content = new StringContent(text, Encoding.UTF8, "text/plain");

        //    content.Headers.Add("ContentType", "text/plain");

        //    return httpClient.PostAsync(url, content);
        //}

    }
}
