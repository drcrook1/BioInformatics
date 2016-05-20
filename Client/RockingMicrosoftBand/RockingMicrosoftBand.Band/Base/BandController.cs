using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Band;
using Microsoft.Band.Sensors;
using RockingMicrosoftBand.Band.Enum;
using RockingMicrosoftBand.Band.IBase;
using RockingMicrosoftBand.Common;
using RockingMicrosoftBand.Common.Base;
using RockingMicrosoftBand.Common.Helpers;
using RockingMicrosoftBand.IOT.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

//TODO" Register events at startup.

namespace RockingMicrosoftBand.Band.Base
{
    public class BandController : ViewModelBase, IDisposable
    {
        //For AQMP Demo
        //public static AMQPSender AQMPSender = new AMQPSender();
        private string _name;

        public ObservableCollection<IAllBandSensorReadings> _allReadings;
        private SensorController<IBandAccelerometerReading> _accelerometerSensor;
        private SensorController<IBandAltimeterReading> _altimeterSensor;
        private SensorController<IBandAmbientLightReading> _ambientLightSensor;
        private BandType _bandType;
        private SensorController<IBandBarometerReading> _barometerSensor;
        private SensorController<IBandCaloriesReading> _calorieSensor;
        private bool _connected;
        private SensorController<IBandContactReading> _contactSensor;
        private string _cSV;
        private ObservableCollection<BandSensorType> _currentListeningSensors;
        private SensorController<IBandDistanceReading> _distanceSensor;
        private string _errorMessage;
        private SensorController<IBandGsrReading> _gsrSensor;
        private SensorController<IBandGyroscopeReading> _gyroscopeSensor;
        private SensorController<IBandHeartRateReading> _heartRateSensor;
        private string _lateststatus;
        private SensorController<IBandPedometerReading> _pedometerSensor;
        private SensorController<IBandRRIntervalReading> _rRIntervalSensor;
        private SensorController<IBandSkinTemperatureReading> _skinTemperatureSensor;
        private SensorController<IBandUVReading> _uVSensor;
        private bool disposed = false;
        private string _iOTHubDeviceConnectionString;
        private string _iOTHubDeviceId;

        
        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

        public string IOTHubDeviceId
        {
            get { return _iOTHubDeviceId; }
            set { Set(() => IOTHubDeviceId, ref _iOTHubDeviceId, value); }
        }
        public string IOTHubDeviceConnectionString
        {
            get { return _iOTHubDeviceConnectionString; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    //can be done Better. 
                    var lower = value.ToLower();
                    var deviceIdentifierLower = "deviceid=";
                    var index = lower.IndexOf(deviceIdentifierLower);
                    var total = index + deviceIdentifierLower.Length;
                    if (index > -1 && value.Length > total)
                    {
                        var rest = value.Substring(total);
                        var indexOfIdEnd = rest.IndexOf(";");
                        if (indexOfIdEnd > -1)
                        {
                            IOTHubDeviceId = rest.Substring(0, indexOfIdEnd);
                        }
                        else
                        {
                            IOTHubDeviceId = rest;
                        }
                    
                    }
                    Set(() => IOTHubDeviceConnectionString, ref _iOTHubDeviceConnectionString, value);
                }
            }
        }

        public BandController()
        {
            if (MicrosoftBandState.BandClient == null)
            {
                this.ErrorMessage = "No Bands Found. You need to connect through BlueTooth then restart the application.";
                return;
            }
            SetBandType();
            SetSensors();
            CurrentListeningSensors = new ObservableCollection<BandSensorType>();
            AllReadings = new ObservableCollection<IAllBandSensorReadings>();
            ErrorMessage = string.Empty;
            CSV = @"Band.csv";
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            IOTHubDeviceId = "Not Set";
            IOTHubDeviceConnectionString = "HostName=band2weuioth.azure-devices.net;DeviceId=Band;SharedAccessKey=aMgxJWSrQIqP3NmWyxguMWgWA+7Pnql65jzDlP0ObEY=;TransportType=Amqp"; //"HostName=band2weuioth.azure-devices.net;DeviceId=Band;SharedAccessSignature=SharedAccessSignature sr=band2weuioth.azure-devices.net%2fdevices%2fBand&sig=QfBnYEfN42RISh3EglSYw5z8J4aX7y%2fnw0wVKN3ybcU%3d&se=1454807816;TransportType=Amqp";
            if (!string.IsNullOrWhiteSpace(CSV))
            {
                FileHelper.DeleteIfExists(CSV);
                FileHelper.CreateFileIfNotExists(CSV, new AnyReading().CSVHeader);
            }
        }

        private async Task SetSensors()
        {
            if (BandType == BandType.Band2)
            {
                AltimeterSensor = SensorFactory.BuildSensorController<IBandSensor<IBandAltimeterReading>, IBandAltimeterReading>("AltimeterSensor", MicrosoftBandState.BandClient.SensorManager.Altimeter, async (args) =>
                {
                    await Trigger(args);
                });
                AmbientLightSensor = SensorFactory.BuildSensorController<IBandSensor<IBandAmbientLightReading>, IBandAmbientLightReading>("AmbientLightSensor", MicrosoftBandState.BandClient.SensorManager.AmbientLight, async (args) =>
                {
                    await Trigger(args);
                });
                BarometerSensor = SensorFactory.BuildSensorController<IBandSensor<IBandBarometerReading>, IBandBarometerReading>("BarometerSensor", MicrosoftBandState.BandClient.SensorManager.Barometer, async (args) =>
                {
                    await Trigger(args);
                });
                GsrSensor = SensorFactory.BuildSensorController<IBandSensor<IBandGsrReading>, IBandGsrReading>("GsrSensor", MicrosoftBandState.BandClient.SensorManager.Gsr, async (args) =>
                {
                    await Trigger(args);
                });
                RRIntervalSensor = SensorFactory.BuildSensorController<IBandSensor<IBandRRIntervalReading>, IBandRRIntervalReading>("RRIntervalSensor", MicrosoftBandState.BandClient.SensorManager.RRInterval, async (args) =>
                {
                    await Trigger(args);
                });
            }
            AccelerometerSensor = SensorFactory.BuildSensorController<IBandSensor<IBandAccelerometerReading>, IBandAccelerometerReading>("AccelerometerSensor", MicrosoftBandState.BandClient.SensorManager.Accelerometer, async (args) =>
            {
                await Trigger(args);
            });
            CalorieSensor = SensorFactory.BuildSensorController<IBandSensor<IBandCaloriesReading>, IBandCaloriesReading>("CalorieSensor", MicrosoftBandState.BandClient.SensorManager.Calories, async (args) =>
            {
                await Trigger(args);
            });
            ContactSensor = SensorFactory.BuildSensorController<IBandSensor<IBandContactReading>, IBandContactReading>("ContactSensor", MicrosoftBandState.BandClient.SensorManager.Contact, async (args) =>
            {
                await Trigger(args);
            });
            DistanceSensor = SensorFactory.BuildSensorController<IBandSensor<IBandDistanceReading>, IBandDistanceReading>("DistanceSensor", MicrosoftBandState.BandClient.SensorManager.Distance, async (args) =>
            {
                await Trigger(args);
            });

            GyroscopeSensor = SensorFactory.BuildSensorController<IBandSensor<IBandGyroscopeReading>, IBandGyroscopeReading>("GyroscopeSensor", MicrosoftBandState.BandClient.SensorManager.Gyroscope, async (args) =>
            {
                await Trigger(args);
            });
            HeartRateSensor = SensorFactory.BuildSensorController<IBandSensor<IBandHeartRateReading>, IBandHeartRateReading>("HeartRateSensor", MicrosoftBandState.BandClient.SensorManager.HeartRate, async (args) =>
            {
                await Trigger(args);
            });
            PedometerSensor = SensorFactory.BuildSensorController<IBandSensor<IBandPedometerReading>, IBandPedometerReading>("PedometerSensor", MicrosoftBandState.BandClient.SensorManager.Pedometer, async (args) =>
            {
                await Trigger(args);
            });
            SkinTemperatureSensor = SensorFactory.BuildSensorController<IBandSensor<IBandSkinTemperatureReading>, IBandSkinTemperatureReading>("SkinTemperatureSensor", MicrosoftBandState.BandClient.SensorManager.SkinTemperature, async (args) =>
            {
                await Trigger(args);
            });
            UVSensor = SensorFactory.BuildSensorController<IBandSensor<IBandUVReading>, IBandUVReading>("UVSensor", MicrosoftBandState.BandClient.SensorManager.UV, async (args) =>
            {
                await Trigger(args);
            });
        }

        public SensorController<IBandAccelerometerReading> AccelerometerSensor
        {
            get { return _accelerometerSensor; }
            set { Set(() => AccelerometerSensor, ref _accelerometerSensor, value); }
        }

        public ObservableCollection<IAllBandSensorReadings> AllReadings
        {
            get { return _allReadings; }
            set
            {
                Set(() => AllReadings, ref _allReadings, value);
                RaisePropertyChanged(() => LastReading);
            }
        }

        private string _firmwareVersion;

        public string FirmwareVersion
        {
            get { return _firmwareVersion; }
            set { Set(() => FirmwareVersion, ref _firmwareVersion, value); }
        }

        private string _hardwareVersion;

        public string HardwareVersion
        {
            get { return _hardwareVersion; }
            set { Set(() => HardwareVersion, ref _hardwareVersion, value); }
        }

        public SensorController<IBandAltimeterReading> AltimeterSensor
        {
            get { return _altimeterSensor; }
            set { Set(() => AltimeterSensor, ref _altimeterSensor, value); }
        }

        public SensorController<IBandAmbientLightReading> AmbientLightSensor
        {
            get { return _ambientLightSensor; }
            set { Set(() => AmbientLightSensor, ref _ambientLightSensor, value); }
        }

        public BandType BandType
        {
            get { return _bandType; }
            set
            {
                Set(() => BandType, ref _bandType, value);
                RaisePropertyChanged(() => BandTypeValue);
            }
        }

        public string BandTypeValue
        {
            get { return BandType.ToStringFromEnum(); }
        }

        public SensorController<IBandBarometerReading> BarometerSensor
        {
            get { return _barometerSensor; }
            set { Set(() => BarometerSensor, ref _barometerSensor, value); }
        }

        public SensorController<IBandCaloriesReading> CalorieSensor
        {
            get { return _calorieSensor; }
            set { Set(() => CalorieSensor, ref _calorieSensor, value); }
        }

        public bool Connected
        {
            get { return _connected; }
            set { Set(() => Connected, ref _connected, value); }
        }

        public SensorController<IBandContactReading> ContactSensor
        {
            get { return _contactSensor; }
            set { Set(() => ContactSensor, ref _contactSensor, value); }
        }

        public string CSV
        {
            get { return _cSV; }
            set { Set(() => CSV, ref _cSV, value); }
        }

        public ObservableCollection<BandSensorType> CurrentListeningSensors
        {
            get { return _currentListeningSensors; }
            set { Set(() => CurrentListeningSensors, ref _currentListeningSensors, value); }
        }

        public SensorController<IBandDistanceReading> DistanceSensor
        {
            get { return _distanceSensor; }
            set { Set(() => DistanceSensor, ref _distanceSensor, value); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { Set(() => ErrorMessage, ref _errorMessage, value); }
        }

        public string Errors
        {
            get
            {
                return string.Empty;
                //var sb = new StringBuilder();
                //sb.AppendLine(AccelerometerSensor.Errors);
                //sb.AppendLine(AltimeterSensor.Errors);
                //sb.AppendLine(AmbientLightSensor.Errors);
                //sb.AppendLine(BarometerSensor.Errors);
                //sb.AppendLine(CalorieSensor.Errors);
                //sb.AppendLine(this.DistanceSensor.Errors);
                //sb.AppendLine(this.GsrSensor.Errors);
                //sb.AppendLine(this.GyroscopeSensor.Errors);
                //sb.AppendLine(this.HeartRateSensor.Errors);
                //sb.AppendLine(this.PedometerSensor.Errors);
                //sb.AppendLine(this.RRIntervalSensor.Errors);
                //sb.AppendLine(this.SkinTemperatureSensor.Errors);
                //sb.AppendLine(this.UVSensor.Errors);
                //return sb.ToString();
            }
        }

        public SensorController<IBandGsrReading> GsrSensor
        {
            get { return _gsrSensor; }
            set { Set(() => GsrSensor, ref _gsrSensor, value); }
        }

        public SensorController<IBandGyroscopeReading> GyroscopeSensor
        {
            get { return _gyroscopeSensor; }
            set { Set(() => GyroscopeSensor, ref _gyroscopeSensor, value); }
        }

        public bool HasErrors
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Errors);
            }
        }

        public SensorController<IBandHeartRateReading> HeartRateSensor
        {
            get { return _heartRateSensor; }
            set { Set(() => HeartRateSensor, ref _heartRateSensor, value); }
        }

        public string LastReading
        {
            get
            {
                if (AllReadings.Any())
                {
                    return string.Format("{0}", AllReadings.FirstOrDefault().ToCSV);
                }
                return string.Empty;
            }
        }

        public string LatestStatus
        {
            get { return _lateststatus; }
            private set { Set(() => LatestStatus, ref _lateststatus, value); RaisePropertyChanged(() => LastReading); }
        }

        public SensorController<IBandPedometerReading> PedometerSensor
        {
            get { return _pedometerSensor; }
            set { Set(() => PedometerSensor, ref _pedometerSensor, value); }
        }

        public SensorController<IBandRRIntervalReading> RRIntervalSensor
        {
            get { return _rRIntervalSensor; }
            set { Set(() => RRIntervalSensor, ref _rRIntervalSensor, value); }
        }

        public SensorController<IBandSkinTemperatureReading> SkinTemperatureSensor
        {
            get { return _skinTemperatureSensor; }
            set { Set(() => SkinTemperatureSensor, ref _skinTemperatureSensor, value); }
        }

        public SensorController<IBandUVReading> UVSensor
        {
            get { return _uVSensor; }
            set { Set(() => UVSensor, ref _uVSensor, value); }
        }

        private static DispatcherTimer Timer { get; set; }

        public async Task<BandSensorStartupResult> ActivateListener(IBandClient bandClient, BandSensorType sensorToActivate)
        {
            if (bandClient == null)
            {
                return BandSensorStartupResult.NoBandInformationPresent;
            }

            switch (BandType)
            {
                case BandType.Band:
                case BandType.Band2:
                    return await StartSensor(sensorToActivate);

                case BandType.None:
                default:
                    return BandSensorStartupResult.NotFound;
                    break;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SetBandType()
        {
            FirmwareVersion = await MicrosoftBandState.BandClient.GetFirmwareVersionAsync();
            HardwareVersion = await MicrosoftBandState.BandClient.GetHardwareVersionAsync();
            BandType = BandType.Band2;
            if (HardwareVersion == "9")
            {
                BandType = BandType.Band;
            }
        }

        public void SendUpdateMessage(BandSensorTypeAndValue sensorType)
        {
            Messenger.Default.Send<BandSensorTypeAndValue>(sensorType);
        }

        public async Task ShowApplicationMessageAsync(string message)
        {
          //  await MicrosoftBandState.BandClient.NotificationManager.ShowDialogAsync(Guid.NewGuid(), "RockingMicrosoftBand", message);
        }

        public async Task StartAll()
        {
            await ShowApplicationMessageAsync("Starting All");
            var allValues = EnumerationExtensions.All<BandSensorType>();
            allValues.ForEach(async e =>
            {
                await StartSensor(e);
            });
        }

        public async Task<BandSensorStartupResult> StartSensor(BandSensorType sensorToActivate)
        {
            return await InvocationHelper.ExecuteWithIgnoreException<Task<BandSensorStartupResult>>(async () =>
            {
                if (BandType.IsValidSensor(sensorToActivate))
                {
                    if (CurrentListeningSensors.Contains(sensorToActivate))
                    {
                        return BandSensorStartupResult.AlreadyRunning;
                    }

                    var started = await StartListening(sensorToActivate);
                    if (!started)
                    {
                        return BandSensorStartupResult.ErrorOnStartup;
                    }
                    return BandSensorStartupResult.Started;
                }
                return BandSensorStartupResult.NotFound;
            });
        }

        public async Task StopAll()
        {
            var allSensors = BandSensorLookup.GetSensors(BandType);
            var allValues = EnumerationExtensions.All<BandSensorType>();
            allValues.ForEach(async e =>
            {
                await StopSensor(e);
            });
        }

        private async Task<bool> StopListener(BandSensorType sensorToActivate)
        {
            var baseMessage = "Stopping Listener to {0}";
            var allThisType = AllReadings.Where(e => e.SensorType == sensorToActivate).ToList();
            SendDataAtEnd(allThisType);
            allThisType.ForEach(e => AllReadings.Remove(e));
            try
            {
                if (CurrentListeningSensors.Remove(sensorToActivate))
                {
                    await ShowApplicationMessageAsync(string.Format(baseMessage, sensorToActivate.ToStringFromEnum()));
                    switch (sensorToActivate)
                    {
                        case BandSensorType.Accelerometer:

                            await AccelerometerSensor.StopReading();

                            break;

                        case BandSensorType.Altimeter:
                            await AltimeterSensor.StopReading();
                            break;

                        case BandSensorType.AmbientLight:
                            await AmbientLightSensor.StopReading();
                            break;

                        case BandSensorType.Barometer:

                            await BarometerSensor.StopReading();

                            break;

                        case BandSensorType.Calories:

                            await CalorieSensor.StopReading();

                            break;

                        case BandSensorType.Distance:

                            await DistanceSensor.StopReading();

                            break;

                        case BandSensorType.Gsr:

                            await GsrSensor.StopReading();

                            break;

                        case BandSensorType.Gyroscope:

                            await GyroscopeSensor.StopReading();

                            break;

                        case BandSensorType.HeartRate:

                            await HeartRateSensor.StopReading();

                            break;

                        case BandSensorType.Pedometer:

                            await PedometerSensor.StopReading();

                            break;

                        case BandSensorType.RR:

                            await RRIntervalSensor.StopReading();

                            break;

                        case BandSensorType.SkinTemperature:

                            await SkinTemperatureSensor.StopReading();

                            break;

                        case BandSensorType.UV:

                            await UVSensor.StopReading();

                            break;

                        case BandSensorType.Contact:

                            await ContactSensor.StopReading();

                            break;

                        case BandSensorType.None:
                        case BandSensorType.Sensor:
                        default:
                            break;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                if (e.Message.ToLower().Contains("aborted"))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<BandSensorStopResult> StopSensor(BandSensorType sensorToActivate)
        {
            return await InvocationHelper.ExecuteWithIgnoreException<Task<BandSensorStopResult>>(async () =>
            {
                var stopped = await StopListener(sensorToActivate);
                if (!stopped)
                {
                    return BandSensorStopResult.ErrorOnStop;
                }
                return BandSensorStopResult.Stopped;
            });
        }

        public async Task UpdateEH(IAllBandSensorReadings anyReading)
        {
            //await HTTPEventHubSender.PostCSVAsync("band2weuioth","Band",string.Format("{0}\n\r{1}",  anyReading.CSVHeader, anyReading.ToCSV));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    MicrosoftBandState.BandClient.Dispose();
                }
            }
            disposed = true;
        }

        private void AddErrorMessage(string errorMessage)
        {
            ErrorMessage += string.Format("{0}\n", errorMessage);
        }

        private async Task SendData(IAllBandSensorReadings anyReading)
        {
            await UpdateCSVFile(anyReading);
            await UpdateIOTHub(anyReading);
            await UpdateEH(anyReading);
        }

        private async Task SendDataAtEnd(List<IAllBandSensorReadings> allReadings)
        {
            await UpdateCSVFile(allReadings);
        }

        private async Task SetBand()
        {
            await MicrosoftBandState.BandClient.NotificationManager.VibrateAsync(Microsoft.Band.Notifications.VibrationType.TwoToneHigh);
            await ShowApplicationMessageAsync("Ready To Listen");
        }

        // Arch Notes
        // TODO: figure out EventHelper to check for existance of EventHandler before adding.
        // Also could add TriggeredReading as a generic method, however this would case a type of T lookup and is slower.
        private async Task<bool> StartListening(BandSensorType sensorToActivate)
        {
            try
            {
                if (!CurrentListeningSensors.Contains(sensorToActivate))
                {
                    CurrentListeningSensors.Add(sensorToActivate);
                    var baseMessage = "Listening To {0}";
                    await InvocationHelper.ExecuteWithIgnoreException<Task>(async () =>
                    {
                        switch (sensorToActivate)
                        {
                            case BandSensorType.Accelerometer:
                                await AccelerometerSensor.StartReading();
                                break;

                            case BandSensorType.Altimeter:
                                await AltimeterSensor.StartReading();
                                break;

                            case BandSensorType.AmbientLight:
                                await AmbientLightSensor.StartReading();
                                break;

                            case BandSensorType.Barometer:
                                await BarometerSensor.StartReading();
                                break;

                            case BandSensorType.Calories:
                                await CalorieSensor.StartReading();
                                break;

                            case BandSensorType.Distance:
                                await DistanceSensor.StartReading();
                                break;

                            case BandSensorType.Gsr:
                                await GsrSensor.StartReading();
                                break;

                            case BandSensorType.Gyroscope:
                                await GyroscopeSensor.StartReading();
                                break;

                            case BandSensorType.HeartRate:
                                await HeartRateSensor.StartReading();
                                break;

                            case BandSensorType.Pedometer:

                                await PedometerSensor.StartReading();
                                break;

                            case BandSensorType.RR:
                                await RRIntervalSensor.StartReading();
                                break;

                            case BandSensorType.SkinTemperature:
                                await SkinTemperatureSensor.StartReading();
                                break;

                            case BandSensorType.UV:
                                await UVSensor.StartReading();
                                break;

                            case BandSensorType.Contact:
                                await ContactSensor.StartReading();
                                break;

                            case BandSensorType.None:
                            case BandSensorType.Sensor:
                            default:
                                break;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                if (e.Message.ToLower().Contains("aborted"))
                {
                    return false;
                }
            }
            return true;
        }

        private void Timer_Tick(object sender, object e)
        {
            Timer.Stop();
        }

        private async Task Trigger<T>(T sensorReading) where T : IBandSensorReading
        {
            var anyReading = new AnyReading().PopulateFromSensorReading(sensorReading);
            await ApplicationState.AnyDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
        if (!Timer.IsEnabled)
        {
            try
            {
                UpdateReadingData(anyReading);
            }
            catch (Exception e)
            {
            }
            //TOdo observable shouldnt have to be raised.
            Timer.Start();
        }
    });

            await SendData(anyReading);
        }

        private void UpdateReadingData(IAllBandSensorReadings anyReading)
        {
            AllReadings.Insert(0,anyReading);
            LatestStatus = anyReading.Status;
        }

        private async Task UpdateCSVFile(IAllBandSensorReadings anyReading)
        {
            //if (!string.IsNullOrWhiteSpace(CSV))
            //{
            //    await CSV.CreateIfNotThereAndWrite(anyReading.ToCSV, anyReading.CSVHeader);
            //}
        }

        private async Task UpdateCSVFile(List<IAllBandSensorReadings> anyReading)
        {
            if (!string.IsNullOrWhiteSpace(CSV))
            {
                await CSV.AppendLines(anyReading.Select(e => e.ToCSV));
            }
        }

        private async Task UpdateIOTHub(IAllBandSensorReadings anyReading)
        {
             await anyReading.ToCSV.SendIOTMessage(IOTHubDeviceConnectionString);
            //    await AQMPSender.SendToServiceBusViaAMQP(anyReading.ToCSV);
        }
    }
}