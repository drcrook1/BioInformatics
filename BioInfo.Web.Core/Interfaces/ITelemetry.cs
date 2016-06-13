using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.Core.Interfaces
{
    public interface ITelemetry
    {
        string Serialize();
        ITelemetry DeSerialize(string telemetry);
    }
}
