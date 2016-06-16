using System.Threading.Tasks;
using BioInfo.Web.Core.Interfaces;

namespace BioInfo.Web.Services
{
    public interface IDeviceRegistration
    {
        Task<FunctionResult<bool>> RegisterDeviceAsync(IDevice device);
        Task<FunctionResult<bool>> UnregisterDeviceAsync(IDevice device);
    }
}