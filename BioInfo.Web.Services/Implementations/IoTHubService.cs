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
        private ILoggingService loggingService;

        public IoTHubService(string iotHubConnString, ILoggingService loggingService)
        {
            regManager = RegistryManager.CreateFromConnectionString(iotHubConnString);
            serviceClient = ServiceClient.CreateFromConnectionString(iotHubConnString);
        }

        public Task<FunctionResult<bool>> UnregisterDeviceAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<FunctionResult<bool>> SendDeviceMessageAsync(IDevice device, string message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            try
            {
                await serviceClient.SendAsync(device.GetName(), commandMessage);
            }
            catch(Exception e)
            {
                loggingService.LogException(e);
                return new FunctionResult<bool>(false, e.Message);
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
            try
            {
                azureDevice = await regManager.AddDeviceAsync(new Device(device.GetName()));
            }
            catch (DeviceAlreadyExistsException e)
            {
                azureDevice = await regManager.GetDeviceAsync(device.GetName());
                loggingService.LogException(e);
            }
            catch(Exception e)
            {
                loggingService.LogException(e);
                return new FunctionResult<bool>(false, e.Message);
            }
            return new FunctionResult<bool>(true);
        }
    }
}
