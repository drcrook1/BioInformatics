
using RockingMicrosoftBand.Band.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockingMicrosoftBand.Band.Base
{
    public static class BandSensors
    {
        public const BandSensorType Band1 = BandSensorType.Accelerometer | BandSensorType.Calories | BandSensorType.Gyroscope | BandSensorType.Distance | BandSensorType.HeartRate | BandSensorType.Pedometer | BandSensorType.SkinTemperature
             | BandSensorType.UV | BandSensorType.Contact;
        
        public const BandSensorType Band2 =  BandSensorType.Gsr | BandSensorType.RR | BandSensorType.AmbientLight | BandSensorType.Barometer | BandSensorType.Altimeter;

        public const BandSensorType AllBands = Band1 | Band2;
        //AllBands.Union(new List<BandSensorType> { BandSensorType.Gsr, BandSensorType.RR, BandSensorType.AmbientLight, BandSensorType.Barometer, BandSensorType.Altimeter }).ToList();
    }
}
