using BioInfo.Web.Core.Interfaces;
using System.Threading.Tasks;

namespace BioInfo.Web.Services.Implementations
{
    public interface IRegistrationCoordinator
    {
        Task<FunctionResult<bool>> RegisterDeviceAsync(IDevice device);
        Task<FunctionResult<bool>> UnregisterDeviceAsync(IDevice device);
    }
}