using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioInfo.Web.Core.Interfaces;
using BioInfo.Web.Core.EntityFramework;

namespace BioInfo.Web.Services.Implementations
{
    class SqlDeviceRegistration : IDeviceRegistration
    {
        private BioInfoDBContext deviceRegistry;

        public SqlDeviceRegistration()
        {
            this.deviceRegistry = new BioInfoDBContext();
        }

        public async Task<FunctionResult<bool>> RegisterDeviceAsync(IDevice device)
        {
            try
            {
                bool userExists = deviceRegistry.DomainUsers.Any(x => x.Id == device.DomainUserId);
                if (!userExists)
                {
                    FunctionResult.Fail("There is no register user.  Please register user before registering device.");
                }

                bool bandExists = deviceRegistry.Bands.Any(x => x.IoTHubId == device.Name);
                if (!bandExists)
                {
                    FunctionResult.Fail("Device has already been registered");
                }

                var band = new Band(device.Name, device.DomainUserId);
                deviceRegistry.Bands.Add(band);
                await deviceRegistry.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                FunctionResult.Fail(ex.Message);
            }

            return FunctionResult.Success();
        }

        public async Task<FunctionResult<bool>> UnregisterDeviceAsync(IDevice device)
        {
            try
            {
                var band = new Band(device.Name, device.DomainUserId);
                deviceRegistry.Bands.Remove(band);
                await deviceRegistry.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                FunctionResult.Fail(ex.Message);
            }

            return FunctionResult.Success();
        }
    }
}
