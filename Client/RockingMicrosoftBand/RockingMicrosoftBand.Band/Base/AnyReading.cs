using Microsoft.Band.Sensors;
using RockingMicrosoftBand.Band.Enum;
using RockingMicrosoftBand.Band.IBase;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

// Mark Rowe  Mark@Alineofcode.net no copyright. Just Credit.
namespace RockingMicrosoftBand.Band.Base
{
    [DataContract]
    public class AnyReading : IAllBandSensorReadings
    {
        private string timestampformat = "dddd, MMM dd yyyy HH:mm:ss zzz";

        public AnyReading()
        {
        }

        public string ToUVStatus()
        {
            return string.Format("{0} status : IndexLevel={1} @{2} ",
               SensorType.ToString("G"), IndexLevel.ToString("G"), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandUVReading input)
        {
            SensorType = BandSensorType.UV;
            IndexLevel = input.IndexLevel;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToUVStatus(); };
        }

        public string ToSkinTemperatureStatus()
        {
            return string.Format("{0} status : Tempurature={1}  @{2} ",
               SensorType.ToString("G"), Temperature.ToString(), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandSkinTemperatureReading input)
        {
            SensorType = BandSensorType.SkinTemperature;
            Temperature = input.Temperature;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToSkinTemperatureStatus(); };
        }

        public string ToRRStatus()
        {
            return string.Format("{0} status : Interval={1}  @{2} ",
               SensorType.ToString("G"), Interval.ToString(), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandRRIntervalReading input)
        {
            SensorType = BandSensorType.RR;
            Interval = input.Interval;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToRRStatus(); };
        }

        public string ToPedometerStatus()
        {
            return string.Format("{0} status : TotalSteps={1}  @{2} ",
               SensorType.ToString("G"), TotalSteps.ToString(), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandPedometerReading input)
        {
            SensorType = BandSensorType.Pedometer;
            TotalSteps = input.TotalSteps;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToPedometerStatus(); };
        }

        public string ToHeartRateStatus()
        {
            var output = string.Format("{0} status : HeartRate={1} Quality={2} @{3} ",
               SensorType.ToString("G"), HeartRate.ToString(), Quality.ToString(), Timestamp.ToString(timestampformat));
            return output;
        }

        public AnyReading(IBandHeartRateReading input)
        {
            SensorType = BandSensorType.HeartRate;
            HeartRate = input.HeartRate;
            Quality = input.Quality;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToHeartRateStatus(); };
        }

        public string ToGyroscopeStatus()
        {
            return string.Format("{0} status : AccelerationX={1}  AccelerationY={2}  AccelerationZ={3} AngularVelocityX={4} AngularVelocityY={5} AngularVelocityZ = {6} @ {7} ",
                SensorType.ToString("G"), AccelerationX.ToString(), AccelerationY.ToString(), AccelerationZ.ToString(), AngularVelocityX.ToString(), AngularVelocityY.ToString(), AngularVelocityZ.ToString(), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandGyroscopeReading input)
        {
            SensorType = BandSensorType.Gyroscope;
            AccelerationX = input.AccelerationX;
            AccelerationY = input.AccelerationY;
            AccelerationZ = input.AccelerationZ;
            AngularVelocityX = input.AngularVelocityX;
            AngularVelocityY = input.AngularVelocityY;
            AngularVelocityZ = input.AngularVelocityZ;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToGyroscopeStatus(); };
        }

        public string ToGsrStatus()
        {
            return string.Format("{0} status : Resistance={1}  @{2} ",
             SensorType.ToString("G"), Resistance, Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandGsrReading input)
        {
            SensorType = BandSensorType.Gsr;
            Resistance = input.Resistance;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToGsrStatus(); };
        }

        public string ToDistanceStatus()
        {
            return string.Format("{0} status : CurrentMotion={1}  Pace={2}  Speed={3} TotalDistance={4} @ {5} ",
                SensorType.ToString("G"), CurrentMotion.ToString("G"), Pace.ToString(), Speed.ToString(), TotalDistance.ToString(), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandDistanceReading input)
        {
            SensorType = BandSensorType.Distance;
            CurrentMotion = input.CurrentMotion;
            Pace = input.Pace;
            Speed = input.Speed;
            Timestamp = input.Timestamp;
            TotalDistance = input.TotalDistance;
            SetStatus = () => { return ToDistanceStatus(); };
        }

        public string ToContactStatus()
        {
            return string.Format("{0} status : State={1} @ {2} ",
                SensorType.ToString("G"), State.ToString("G"), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandContactReading input)
        {
            SensorType = BandSensorType.Contact;
            State = input.State;
            Timestamp = input.Timestamp;
               SetStatus = () => { return ToContactStatus(); };
        }

        public string ToCaloriesStatus()
        {
            return string.Format("{0} status : Calories={1} @ {2}",
                SensorType.ToString("G"), Calories.ToString(), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandCaloriesReading input)
        {
            SensorType = BandSensorType.Calories;
            Calories = input.Calories;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToCaloriesStatus(); };
        }

        public string ToBarometerStatus()
        {
            return string.Format("{0} status : AirPressure={1} Temperature={2} @ {3}",
                SensorType.ToString("G"), AirPressure.ToString(), Temperature.ToString(), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandBarometerReading input)
        {
            SensorType = BandSensorType.Barometer;
            AirPressure = input.AirPressure;
            Temperature = input.Temperature;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToBarometerStatus(); };
        }

        public string ToAmbientLightStatus()
        {
            return string.Format("{0} status : Brightness={1} @ {2}",
                SensorType.ToString("G"), Brightness.ToString(), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandAmbientLightReading input)
        {
            SensorType = BandSensorType.AmbientLight;
            Brightness = input.Brightness;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToAmbientLightStatus(); };
        }

        public string ToAccelerometerStatus()
        {
            return string.Format("{0} status : AccelerationX={1}  AccelerationY={2}  AccelerationZ={3} @ {4} ",
                SensorType.ToString("G"), AccelerationX.ToString(), AccelerationY.ToString(), AccelerationZ.ToString(), Timestamp.ToString(timestampformat));
        }

        public AnyReading(IBandAccelerometerReading input)
        {
            SensorType = BandSensorType.Accelerometer;
            AccelerationX = input.AccelerationX;
            AccelerationY = input.AccelerationY;
            AccelerationZ = input.AccelerationZ;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToAccelerometerStatus(); };
        }

        public string ToAltimeterStatus()
        {
            return string.Format("{0} status : FlightsAscended={1}  FlightsDescended={2}  SteppingGain={3} SteppingLoss={4} StepsAscended={5} StepsDescended={6} TotalGain={7} TotalLoss={8} @ {9} ",
                 SensorType.ToString("G"), FlightsAscended.ToString(), FlightsDescended.ToString(), SteppingGain.ToString(), SteppingLoss.ToString(),
            StepsAscended.ToString(), StepsDescended.ToString(), TotalGain.ToString(), TotalLoss.ToString(), Timestamp.ToString(timestampformat));
        }
        public AnyReading(IBandSensorReading input)
        {
            Timestamp = input.Timestamp;
        }
        public string Status
        {
            get { return SetStatus.Invoke(); }
        }
        public Func<string> SetStatus
        {
            get; set;
        }
        public IAllBandSensorReadings PopulateFromSensorReading<T>(T reading)
        {
            var localType = typeof(T);
            if (localType == typeof(IBandAccelerometerReading))
            {
                return new AnyReading(reading as IBandAccelerometerReading);
            }
            if (localType == typeof(IBandAccelerometerReading))
            {
                return new AnyReading(reading as IBandAccelerometerReading);
            }
            if (localType == typeof(IBandAltimeterReading))
            {
                return new AnyReading(reading as IBandAltimeterReading);
            }
            if (localType == typeof(IBandAmbientLightReading))
            {
                return new AnyReading(reading as IBandAmbientLightReading);
            }
            if (localType == typeof(IBandBarometerReading))
            {
                return new AnyReading(reading as IBandBarometerReading);
            }
            if (localType == typeof(IBandCaloriesReading))
            {
                return new AnyReading(reading as IBandCaloriesReading);
            }
            if (localType == typeof(IBandContactReading))
            {
                return new AnyReading(reading as IBandContactReading);
            }
            if (localType == typeof(IBandDistanceReading))
            {
                return new AnyReading(reading as IBandDistanceReading);
            }
            if (localType == typeof(IBandGsrReading))
            {
                return new AnyReading(reading as IBandGsrReading);
            }
            if (localType == typeof(IBandGyroscopeReading))
            {
                return new AnyReading(reading as IBandGyroscopeReading);
            }
            if (localType == typeof(IBandHeartRateReading))
            {
                return new AnyReading(reading as IBandHeartRateReading);
            }
            if (localType == typeof(IBandPedometerReading))
            {
                return new AnyReading(reading as IBandPedometerReading);
            }
            if (localType == typeof(IBandRRIntervalReading))
            {
                return new AnyReading(reading as IBandRRIntervalReading);
            }
            if (localType == typeof(IBandSkinTemperatureReading))
            {
                return new AnyReading(reading as IBandSkinTemperatureReading);
            }
            if (localType == typeof(IBandUVReading))
            {
                return new AnyReading(reading as IBandUVReading);
            }
                    return this;
        }

        public AnyReading(IBandAltimeterReading input)
        {
            SensorType = BandSensorType.Altimeter;
            FlightsAscended = input.FlightsAscended;
            FlightsDescended = input.FlightsDescended;
            Rate = input.Rate;
            SteppingGain = input.SteppingGain;
            SteppingLoss = input.SteppingLoss;
            StepsAscended = input.StepsAscended;
            StepsDescended = input.StepsDescended;
            TotalGain = input.TotalGain;
            TotalLoss = input.TotalLoss;
            Timestamp = input.Timestamp;
            SetStatus = () => { return ToAltimeterStatus(); };
        }

        [DataMember]
        public BandSensorType SensorType { get; set; }

        [DataMember]
        public double AccelerationX { get; set; }

        [DataMember]
        public double AccelerationY { get; set; }

        [DataMember]
        public double AccelerationZ { get; set; }

        [DataMember]
        public double AirPressure { get; set; }

        [DataMember]
        public double AngularVelocityX { get; set; }

        [DataMember]
        public double AngularVelocityY { get; set; }

        [DataMember]
        public double AngularVelocityZ { get; set; }

        [DataMember]
        public int Brightness { get; set; }

        [DataMember]
        public long Calories { get; set; }

        [DataMember]
        public MotionType CurrentMotion { get; set; }

        [DataMember]
        public long FlightsAscended { get; set; }

        [DataMember]
        public long FlightsDescended { get; set; }

        [DataMember]
        public int HeartRate { get; set; }

        [DataMember]
        public UVIndexLevel IndexLevel { get; set; }

        [DataMember]
        public double Interval { get; set; }

        [DataMember]
        public double Pace { get; set; }

        [DataMember]
        public HeartRateQuality Quality { get; set; }

        [DataMember]
        public float Rate { get; set; }

        [DataMember]
        public int Resistance { get; set; }

        [DataMember]
        public double Speed { get; set; }

        [DataMember]
        public BandContactState State { get; set; }

        [DataMember]
        public long SteppingGain { get; set; }

        [DataMember]
        public long SteppingLoss { get; set; }

        [DataMember]
        public long StepsAscended { get; set; }

        [DataMember]
        public long StepsDescended { get; set; }

        [DataMember]
        public double Temperature { get; set; }

        [DataMember]
        public DateTimeOffset Timestamp { get; set; }

        [DataMember]
        public long TotalDistance { get; set; }

        [DataMember]
        public long TotalGain { get; set; }

        [DataMember]
        public long TotalLoss { get; set; }

        [DataMember]
        public long TotalSteps { get; set; }

        public string CSVHeader //32
        {
            get { return "SensorType,AccelerationX,AccelerationY,AccelerationZ,AirPressure,AngularVelocityX,AngularVelocityY,AngularVelocityZ,Brightness,Calories,CurrentMotion,FlightsAscended,FlightsDescended,HeartRate,IndexLevel,Interval,Pace,Quality,Rate,Resistance,Speed,State,SteppingGain,SteppingLoss,StepsAscended,StepsDescended,Temperature,Timestamp,TotalDistance,TotalGain,TotalLoss,TotalSteps"; }
        }

        public string ToCSV
        {
            get
            {
                var output = string.Empty;

                try
                {
                    output = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31}",
                SensorType != BandSensorType.None ? SensorType.ToString("G") : string.Empty, AccelerationX.ToString(), AccelerationY.ToString(), AccelerationZ.ToString(), AirPressure.ToString(), AngularVelocityX.ToString(),
                AngularVelocityY.ToString(), AngularVelocityZ.ToString(), Brightness.ToString(), Calories.ToString(), CurrentMotion == MotionType.Unknown ? CurrentMotion.ToString("G") : string.Empty,
                FlightsAscended.ToString(), FlightsDescended.ToString(), HeartRate.ToString(), SensorType == BandSensorType.UV ? IndexLevel.ToString("G") : string.Empty, Interval.ToString(), Pace.ToString(),
                 SensorType == BandSensorType.HeartRate ? Quality.ToString("G") : string.Empty, Rate.ToString(), Resistance.ToString(), Speed.ToString(),
               SensorType == BandSensorType.Contact ? State.ToString("G") : string.Empty, SteppingGain.ToString(), SteppingLoss.ToString(), StepsAscended.ToString(), StepsDescended.ToString(), Temperature.ToString(),
               Timestamp != null ? Timestamp.ToString(timestampformat) : string.Empty, TotalDistance.ToString(), TotalGain.ToString(), TotalLoss.ToString(), TotalSteps.ToString());
                }
                catch (Exception ex)
                {
                }
                return output;
            }
        }
    }
}