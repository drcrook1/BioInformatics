using Microsoft.Band.Sensors;
using RockingMicrosoftBand.Band.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockingMicrosoftBand.Band.IBase
{
    public interface IAllBandSensorReadings : IBandAccelerometerReading, IBandAltimeterReading, IBandAmbientLightReading, IBandBarometerReading, IBandCaloriesReading, IBandContactReading,
        IBandDistanceReading, IBandGsrReading, IBandGyroscopeReading, IBandHeartRateReading, IBandPedometerReading, IBandRRIntervalReading, IBandSkinTemperatureReading, IBandUVReading
    {
        BandSensorType SensorType { get; set; }
        string ToCSV { get; }
        string CSVHeader { get;}
        IAllBandSensorReadings PopulateFromSensorReading<T>(T reading);
        string Status { get; }
    }
}
