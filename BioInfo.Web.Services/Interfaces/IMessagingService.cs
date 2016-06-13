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
        bool AssignSecurityTokenToDevice();
        bool RevokeSecurityTokenFromDevice();
        bool SendDeviceNotification(IDevice device, IMessage message);
    }
}
