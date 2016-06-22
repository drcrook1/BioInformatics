using BioInfo.Web.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.Services.Implementations
{
    public class RegistrationCoordinator
    {
        private IDeviceRegistration ioTHubRegister;
        private IDeviceRegistration sqlRegister;

        public RegistrationCoordinator(IDeviceRegistration iotHubRegister, IDeviceRegistration sqlRegister)
        {
            this.ioTHubRegister = iotHubRegister;
            this.sqlRegister = sqlRegister;
        }


        public async Task<FunctionResult<bool>> RegisterDevice(IDevice device)
        {
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

    }
}
