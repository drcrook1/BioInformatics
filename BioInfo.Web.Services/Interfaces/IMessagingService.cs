using BioInfo.Web.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.Services
{
    public interface IMessagingService
    {
        Task<FunctionResult<bool>> RegisterDeviceAsync(IDevice device);
        Task<FunctionResult<bool>> UnregisterDeviceAsync();
        Task<FunctionResult<bool>> SendDeviceMessageAsync(IDevice device, string message);
    }
}
