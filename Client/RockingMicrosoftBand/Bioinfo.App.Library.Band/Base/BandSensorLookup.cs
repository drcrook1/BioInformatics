
using Bioinfo.App.Library.Band.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinfo.App.Library.Band.Base
{
    public static class BandSensorLookup
    {
        public static BandSensorType GetSensors(this BandType bandType)
        {
            //could just return, but cleaner and small amount of data (list of up to 20 ints). 
            var output = BandSensorType.None;
            switch (bandType)
            {
                case BandType.Band:
                    output = BandSensors.Band1;
                    break;
                case BandType.Band2:
                    output = BandSensors.Band2 | BandSensors.Band1;
                    break;
            }
            return output;
        }
        public static bool IsValidSensor(this BandType bandType, BandSensorType bandSensorType)
        {
            return bandType.GetSensors().HasFlag(bandSensorType);
        }
    }
}
