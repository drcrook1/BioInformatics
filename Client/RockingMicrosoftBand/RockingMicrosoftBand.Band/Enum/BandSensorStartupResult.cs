using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockingMicrosoftBand.Band.Enum
{
    public enum BandSensorStartupResult : int
    {
        None, 
        NotFound,
        NoConsent,
        NoBandInformationPresent, 
        NoBandClientPresent,
        ErrorOnStartup, 
        AlreadyRunning,
        Started
    }
}
