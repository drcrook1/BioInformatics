using BioInfo.Web.Core.Interfaces;
using BioInfo.Web.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.Services.Implementations
{
    public class SimpleRegistrationCoordinator : IRegistrationCoordinator
    {
        private IDeviceRegistration ioTHubRegister;
        private IDeviceRegistration sqlRegister;

        public SimpleRegistrationCoordinator(IDeviceRegistration iotHubRegister, IDeviceRegistration sqlRegister)
        {
            this.ioTHubRegister = iotHubRegister;
            this.sqlRegister = sqlRegister;
        }


        public async Task<FunctionResult<bool>> RegisterDeviceAsync(IDevice device)
        {
            // Kept this registration faily simple for now.  
            // There are many scenarios to consider such partial registration that could potentially
            // handled via more complex logic.  
            var sqlResult = await sqlRegister.RegisterDeviceAsync(device);
            if (sqlResult.DidFail())
            {
                return FunctionResult.Fail($"Registration Failed: {sqlResult.GetFriendlyError()}");
            }

            var iotResult = await ioTHubRegister.RegisterDeviceAsync(device);
            if (iotResult.DidFail())
            {
                return FunctionResult.Fail($"Partial Registration occurred. Device was not registered in IoT Hub but was regirstered localy. {iotResult.GetFriendlyError()}");
            }

            return FunctionResult.Success();
        }

        public async Task<FunctionResult<bool>> UnregisterDeviceAsync(IDevice device)
        {
            // Kept this registration faily simple for now.  
            var sqlResult = await sqlRegister.UnregisterDeviceAsync(device);
            if (sqlResult.DidFail())
            {
                return FunctionResult.Fail($"Registration Failed: {sqlResult.GetFriendlyError()}");
            }

            var iotResult = await ioTHubRegister.UnregisterDeviceAsync(device);
            if (iotResult.DidFail())
            {
                return FunctionResult.Fail($"Partial Registration occurred. Device was not registered in IoT Hub but was regirstered localy. {iotResult.GetFriendlyError()}");
            }

            return FunctionResult.Success();
        }

       

    }
}
