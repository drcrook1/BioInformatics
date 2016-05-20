using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinfo.App.Library.Band.Enum
{
    [Flags]
    public enum BandSensorType : int
    {

        None = 0,
        Accelerometer = 1,
        Altimeter = 2,
        AmbientLight = 4,
        Barometer = 8,
        Calories = 16,
        Distance = 32,
        Gsr = 64,
        Gyroscope = 128,
        HeartRate = 256,
        Pedometer = 512,
        RR = 1024,
        SkinTemperature = 2048,
        UV = 4096,
        Contact = 8192,
        Sensor = 16384,
    }
}
