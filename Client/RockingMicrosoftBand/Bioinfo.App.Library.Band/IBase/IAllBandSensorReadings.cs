using Microsoft.Band.Portable.Sensors;
using Bioinfo.App.Library.Band.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band.Sensors;

namespace Bioinfo.App.Library.Band.IBase
{
    public interface IAllBandSensorReadings : IBandAccelerometerReading, IBandAltimeterReading, IBandAmbientLightReading, IBandBarometerReading, IBandCaloriesReading, IBandContactReading,
           IBandDistanceReading, IBandGsrReading, IBandGyroscopeReading, IBandHeartRateReading, IBandPedometerReading, IBandRRIntervalReading, IBandSkinTemperatureReading, IBandUVReading
    {
        BandSensorType SensorType { get; set; }
        string ToCSV { get; }
        string CSVHeader { get; }
        IAllBandSensorReadings PopulateFromSensorReading<T>(T reading);
        string Status { get; }
    }
}
