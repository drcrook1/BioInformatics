using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioInfo.Web.Core.Interfaces;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using BioInfo.Web.Services.Interfaces;

namespace BioInfo.Web.Services.Implementations
{
    public class IoTHubService : IMessagingService
    {
        private static RegistryManager regManager;
        private static ServiceClient serviceClient;

        public IoTHubService(string iotHubConnString)
        {
            regManager = RegistryManager.CreateFromConnectionString(iotHubConnString);
            serviceClient = ServiceClient.CreateFromConnectionString(iotHubConnString);
        }

        public async Task<FunctionResult<bool>> UnregisterDeviceAsync(IDevice device)
        {
            var devName = device.GetName().GetResult();
            try
            {
                await regManager.RemoveDeviceAsync(devName);
            }
            catch(Exception e)
            {
                return new FunctionResult<bool>(false, e.Message, true);
            }
            return new FunctionResult<bool>(true);
        }

        public async Task<FunctionResult<bool>> SendDeviceMessageAsync(IDevice device, string message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            try
            {
                var devName = device.GetName().GetResult();
                await serviceClient.SendAsync(devName, commandMessage);
            }
            catch(Exception e)
            {
                return new FunctionResult<bool>(false, e.Message, true);
            }
            return new FunctionResult<bool>(true);

        }

        /// <summary>
        /// Registers new device with Azure IoT Hub.
        /// 
        /// Returns Function Result True if successful
        /// Returns Function Result False  + Error if unsuccessful.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<FunctionResult<bool>> RegisterDeviceAsync(IDevice device)
        {
            Device azureDevice;
            var devName = device.GetName().GetResult();
            try
            {                
                azureDevice = await regManager.AddDeviceAsync(new Device(devName));
            }
            catch (DeviceAlreadyExistsException e)
            {
                azureDevice = await regManager.GetDeviceAsync(devName);
            }
            catch(Exception e)
            {
                return new FunctionResult<bool>(false, e.Message, true);
            }
            return new FunctionResult<bool>(true);
        }
    }
}
