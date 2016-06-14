using BioInfo.Web.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.Services.Interfaces
{
    public interface ILoggingService
    {
        void LogMessage(IAppLogMessage message);
        void LogException(Exception e);
    }
}
