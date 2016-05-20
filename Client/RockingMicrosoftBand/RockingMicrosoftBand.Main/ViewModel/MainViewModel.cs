using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Band;
using RockingMicrosoftBand.Band;
using RockingMicrosoftBand.Band.Base;
using RockingMicrosoftBand.Band.Enum;
using RockingMicrosoftBand.Common;
using RockingMicrosoftBand.Common.Base;
using RockingMicrosoftBand.Common.Helpers;
using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace RockingMicrosoftBand.Main.ViewModel
{
    /// <summary>
    /// TODO: Create reusable patterns. Seems like I can break out a lot of logic into individual classes and cut code over 50% (Commands) 
    /// Fails my smell tests. 
    /// 
    /// </summary>
    public class MainViewModel : ViewModelBase// ViewModelStandIn
    {
        private static BandController _microsoftBand;
        //TODO: Throw in some config.
        private static Brush Band2Color = new SolidColorBrush(Colors.Yellow);
        private static Brush Band1Color = new SolidColorBrush(Colors.Gray);
        private static Brush AccelerometerStartColor = new SolidColorBrush(Colors.Orange);
        private static Brush AltimiterStartColor = new SolidColorBrush(Colors.DarkOrchid);
        private static Brush AmbientLightingStartColor = new SolidColorBrush(Colors.Yellow);
        private static Brush BarometerStartColor = new SolidColorBrush(Colors.YellowGreen);
        private static Brush CaloriesStartColor = new SolidColorBrush(Colors.Green);
        private static Brush ContactStartColor = new SolidColorBrush(Colors.Blue);
        private static Brush DistanceStartColor = new SolidColorBrush(Colors.Violet);
        private static Brush GsrStartColor = new SolidColorBrush(Colors.Purple);
        private static Brush GyroscopeStartColor = new SolidColorBrush(Colors.MediumPurple);
        private static Brush HeartRateStartColor = new SolidColorBrush(Colors.MidnightBlue);
        private static Brush PedometerStartColor = new SolidColorBrush(Colors.HotPink);
        private static Brush RRInervalStartColor = new SolidColorBrush(Colors.LimeGreen);
        private static Brush SkinTemperatureStarColor = new SolidColorBrush(Colors.Yellow);
        private static Brush StartColor = new SolidColorBrush(Colors.Green);
        private static Brush StopColor = new SolidColorBrush(Colors.Red);
        private static Brush UVStartColor = new SolidColorBrush(Colors.DeepSkyBlue);
        private Brush _backgroundAccelerometer;
        private Brush _backgroundAltimeter;
        private Brush _backgroundAmbientLighting;
        private Brush _backgroundBarometer;
        private Brush _backgroundCalories;
        private Brush _backgroundContact;
        private Brush _backgroundDistance;
        private Brush _backgroundGSR;
        private Brush _backgroundGyroscope;
        private Brush _backgroundHeartRate;
        private Brush _backgroundPedometer;
        private Brush _backgroundRRInterval;
        private Brush _backgroundSkinTemperature;
        private Brush _backgroundUVReading;
        private string _runText;
        private string isRunning = "Stop Listening";
        private string isStopped = "Start Listening";

        public MainViewModel() : base()
        {
            ApplicationState.AnyDispatcher = Window.Current.CoreWindow.Dispatcher;
            InitButtons();
            RunText = isStopped;
            SetBand();
        }

        public bool IsConnected
        {
            get
            {
                return MicrosoftBandState.BandClient != null;
            }
        }
        //I  can do better than checking a color.  
        public bool AccelerometerrRunning
        {
            get { return BackgroundAccelerometer == StopColor; }
        }

        public bool AltimeterRunning
        {
            get { return BackgroundAltimeter == StopColor; }
        }

        public bool AmbientLightingRunning
        {
            get { return BackgroundAmbientLighting == StopColor; }
        }

        public Brush BackgroundAccelerometer
        {
            get { return _backgroundAccelerometer; }
            set
            {
                Set(() => BackgroundAccelerometer, ref _backgroundAccelerometer, value);
            }
        }

        public Brush BackgroundAltimeter
        {
            get { return _backgroundAltimeter; }
            set
            {
                Set(() => BackgroundAltimeter, ref _backgroundAltimeter, value);
            }
        }

        public Brush BackgroundAmbientLighting
        {
            get { return _backgroundAmbientLighting; }
            set
            {
                Set(() => BackgroundAmbientLighting , ref _backgroundAmbientLighting, value);
            }
        }

        public Brush BackgroundBarometer
        {
            get { return _backgroundBarometer; }
            set
            {
                Set(() => BackgroundBarometer, ref _backgroundBarometer, value);
            }
        }

        public Brush BackgroundCalories
        {
            get { return _backgroundCalories; }
            set
            {
                Set(() => BackgroundCalories, ref _backgroundCalories, value);
            }
        }

        public Brush BackgroundContact
        {
            get { return _backgroundContact; }
            set
            {
                Set(() => BackgroundContact, ref _backgroundContact, value);
            }
        }

        public Brush BackgroundDistance
        {
            get { return _backgroundDistance; }
            set
            {
                Set(() => BackgroundDistance, ref _backgroundDistance, value);
            }
        }

        public Brush BackgroundGSR
        {
            get { return _backgroundGSR; }
            set
            {
                Set(() => BackgroundGSR, ref _backgroundGSR, value);
            }
        }

        public Brush BackgroundGyroscope
        {
            get { return _backgroundGyroscope; }
            set
            {
                Set(() => BackgroundGyroscope, ref _backgroundGyroscope, value);
            }
        }

        public Brush BackgroundHeartRate
        {
            get { return _backgroundHeartRate; }
            set
            {
                Set(() => BackgroundHeartRate, ref _backgroundHeartRate, value);
            }
        }

        public Brush BackgroundPedometer
        {
            get { return _backgroundPedometer; }
            set
            {
                Set(() => BackgroundPedometer, ref _backgroundPedometer, value);
            }
        }

        public Brush BackgroundRRInterval
        {
            get { return _backgroundRRInterval; }
            set
            {
                Set(() => BackgroundRRInterval, ref _backgroundRRInterval, value);
            }
        }

        public Brush BackgroundSkinTemperature
        {
            get { return _backgroundSkinTemperature; }
            set
            {
                Set(() => BackgroundSkinTemperature, ref _backgroundSkinTemperature, value);
            }
        }

        public Brush BackgroundUVReading
        {
            get { return _backgroundUVReading; }
            set
            {
                Set(() => BackgroundUVReading, ref _backgroundUVReading, value);
            }
        }

        public bool BarometerRunning
        {
            get { return BackgroundBarometer == StopColor; }
        }

        public bool CaloriesRunning
        {
            get { return BackgroundCalories == StopColor; }
        }

        public bool ContactRunning
        {
            get { return BackgroundContact == StopColor; }
        }

        public CoreDispatcher Dispatcher { get; set; }

        public bool DistanceRunning
        {
            get { return BackgroundDistance == StopColor; }
        }

        public FontFamily FontFamilyAccelerometer { get; set; }

        public FontFamily FontFamilyAltimeter { get; set; }

        public FontFamily FontFamilyAmbientLighting { get; set; }

        public FontFamily FontFamilyBarometer { get; set; }

        public FontFamily FontFamilyCalories { get; set; }

        public FontFamily FontFamilyContact { get; set; }

        public FontFamily FontFamilyDistance { get; set; }

        public FontFamily FontFamilyGSR { get; set; }

        public FontFamily FontFamilyGyroscope { get; set; }

        public FontFamily FontFamilyHeartRate { get; set; }

        public FontFamily FontFamilyPedometer { get; set; }

        public FontFamily FontFamilyRRInterval { get; set; }

        public FontFamily FontFamilySkinTemperature { get; set; }

        public FontFamily FontFamilyUVReading { get; set; }

        public bool GSRRunning
        {
            get { return BackgroundGSR == StopColor; }
        }

        public bool GyroscopeRunning
        {
            get { return BackgroundGyroscope == StopColor; }
        }

        public bool HeartRateRunning
        {
            get { return BackgroundHeartRate == StopColor; }
        }

        public BandController MicrosoftBand
        {

            get { return _microsoftBand; }
            set
            {
                Set(() => MicrosoftBand, ref _microsoftBand, value);
            }
        }

        public bool PedometerRunning
        {
            get { return BackgroundPedometer == StopColor; }
        }

        public bool RRIntervalRunning
        {
            get { return BackgroundRRInterval == StopColor; }
        }

        public RelayCommand RunCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    try
                    {
                        if (IsRunning)
                        {
                            RunText = isStopped;
                            await MicrosoftBand.StopAll();
                            return;
                        }
                        RunText = isRunning;
                        await MicrosoftBand.StartAll();
                        return;
                    }
                    catch (Exception ex)
                    {
                    }
                });
            }
        }

        public string RunTextAccelerometer { get; set; }
        private string _textBand;
        public string TextBand
        {
            get { return _textBand; }
            set
            {
                Set(() => TextBand, ref _textBand, value);
            }
        }
        private FontFamily _fontFamilyBand;
        public FontFamily FontFamilyBand
        {
            get { return _fontFamilyBand; }
            set
            {
                Set(() => FontFamilyBand, ref _fontFamilyBand, value);
            }
        }
        public string RunText
        {
            get { return _runText; }
            set
            {
                Set(() => RunText, ref _runText, value);
            }
        }

        public string RunTextAltimeter { get; set; }

        public string RunTextAmbientLighting { get; set; }

        public string RunTextBarometer { get; set; }

        public string RunTextCalories { get; set; }

        public string RunTextContact { get; set; }

        public string RunTextDistance { get; set; }

        public string RunTextGSR { get; set; }

        public string RunTextGyroscope { get; set; }

        public string RunTextHeartRate { get; set; }

        public string RunTextPedometer { get; set; }

        public string RunTextRRInterval { get; set; }

        public string RunTextSkinTemperature { get; set; }

        public string RunTextUVReading { get; set; }

        public bool SkinTemperatureRunning
        {
            get { return BackgroundSkinTemperature == StopColor; }
        }

        public RelayCommand StartOrStopAccelerometer
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.Accelerometer;
                    if (!AccelerometerrRunning)
                    {
                        BackgroundAccelerometer = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                          await  ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundAccelerometer = AccelerometerStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }
        private async Task ResetAndRestop(BandSensorType anyType)
        {
            await SetBand();
            var result = await MicrosoftBand.StopSensor(anyType);
            if (result == BandSensorStopResult.ErrorOnStop)
            {
                //TODO: Fatal Error
            }
        }
        private async Task ResetAndRestart(BandSensorType anyType)
        {
            await SetBand();
            var result = await MicrosoftBand.StartSensor(anyType);
            if (result == BandSensorStartupResult.ErrorOnStartup)
            {
               //TODO: Fatal Error
            }
        }

        public RelayCommand StartOrStopAltimeter
        {
            get
            {
              
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.Altimeter;
                    if (!AltimeterRunning)
                    {
                        BackgroundAltimeter = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                          await  ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundAltimeter = AltimiterStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopAmbientLighting
        {
            get
            {
                
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.AmbientLight;
                    if (!AmbientLightingRunning)
                    {
                        BackgroundAmbientLighting = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundAmbientLighting = AmbientLightingStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopBarometer
        {
            get
            {
                
                return new RelayCommand(async () =>
                       {
                           var sensorType = BandSensorType.Barometer;
                           if (!BarometerRunning)
                           {
                               BackgroundBarometer = StopColor;
                               var startResult = await MicrosoftBand.StartSensor(sensorType);
                               if (startResult == BandSensorStartupResult.ErrorOnStartup)
                               {
                                   await ResetAndRestart(sensorType);
                               }
                               return;
                           }
                           BackgroundBarometer = BarometerStartColor;
                           var stopResult = await MicrosoftBand.StopSensor(sensorType);
                           if (stopResult == BandSensorStopResult.ErrorOnStop)
                           {
                               await ResetAndRestop(sensorType);
                           }
                           return;
                       });
            }
        }

        public RelayCommand StartOrStopCalories
        {
           
            get
            {
            
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.Calories;
                    if (!CaloriesRunning)
                    {
                        BackgroundCalories = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundCalories = CaloriesStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        private string _deviceId;

        public string DeviceId
        {
            get { return _deviceId; }
            set { Set(() => DeviceId, ref _deviceId, value); }
        }        
        public RelayCommand StartOrStopContact
        {
            get
            {
            
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.Contact;
                    if (!ContactRunning)
                    {
                        BackgroundContact = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundContact = ContactStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopDistance
        {
            get
            {
               
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.Distance;
                    if (!DistanceRunning)
                    {
                        BackgroundDistance = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundDistance = DistanceStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopGSR
        {
            get
            {
               
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.Gsr;
                    if (!GSRRunning)
                    {
                        BackgroundGSR = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundGSR = GsrStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopGyroscope
        {
            get
            {
                 
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.Gyroscope;
                    if (!GyroscopeRunning)
                    {
                        BackgroundGyroscope = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundGyroscope = GyroscopeStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopHeartRate
        {
            get
            {

               
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.HeartRate;
                    if (!HeartRateRunning)
                    {
                        BackgroundHeartRate = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundHeartRate = HeartRateStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopPedometer
        {
            get
            {
                
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.Pedometer;
                    if (!PedometerRunning)
                    {
                        BackgroundPedometer = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundPedometer = PedometerStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopRRInterval
        {
            get
            {
               
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.RR;
                    if (!RRIntervalRunning)
                    {
                        BackgroundRRInterval = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundRRInterval = RRInervalStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopSkinTemperature
        {
            get
            {
              
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.SkinTemperature;
                    if (!SkinTemperatureRunning)
                    {
                        BackgroundSkinTemperature = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundSkinTemperature = SkinTemperatureStarColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }

        public RelayCommand StartOrStopUVReading
        {
            get
            {
              
                return new RelayCommand(async () =>
                {
                    var sensorType = BandSensorType.UV;
                    if (!UVReadingRunning)
                    {
                        BackgroundUVReading = StopColor;
                        var startResult = await MicrosoftBand.StartSensor(sensorType);
                        if (startResult == BandSensorStartupResult.ErrorOnStartup)
                        {
                            await ResetAndRestart(sensorType);
                        }
                        return;
                    }
                    BackgroundUVReading = UVStartColor;
                    var stopResult = await MicrosoftBand.StopSensor(sensorType);
                    if (stopResult == BandSensorStopResult.ErrorOnStop)
                    {
                        await ResetAndRestop(sensorType);
                    }
                    return;
                });
            }
        }
        //TODO finish:
        //public RelayCommand BuildRelayCommandForSensor(BandSensorType sensor, bool running, ref Brush brushToSet, Brush startColor)
        //{
        //      return new RelayCommand(async () =>
        //        {
        //            if (!UVReadingRunning)
        //            {
        //                brushToSet = StopColor;
        //                var startResult = await MicrosoftBand.StartSensor(BandSensorType.UV);
        //                if (startResult == BandSensorStartupResult.ErrorOnStartup)
        //                {
        //                    await ResetAndRestart(BandSensorType.Accelerometer);
        //                }
        //                return;
        //            }
        //            brushToSet = startColor;
        //            var stopResult = await MicrosoftBand.StopSensor(BandSensorType.UV);
        //            if (stopResult == BandSensorStopResult.ErrorOnStop)
        //            {
        //                await ResetAndRestop(BandSensorType.Accelerometer);
        //            }
        //            return;
        //        });

        //}
        public Visibility Band1Visibility
        {
            get
            {
                if (MicrosoftBandState.BandClient != null && MicrosoftBand != null && (MicrosoftBand.BandType == BandType.Band || MicrosoftBand.BandType == BandType.Band2))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility Band2Visibility
        {
            get
            {
                if (MicrosoftBandState.BandClient != null && MicrosoftBand != null && MicrosoftBand.BandType == BandType.Band2)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public bool UVReadingRunning
        {
            get { return BackgroundUVReading == StopColor; }
        }

        private bool IsRunning
        {
            get { return RunText == isRunning; }
        }

        public async Task SetBand()
        {
            await ApplicationState.AnyDispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
             await   InvocationHelper.ExecuteWithIgnoreException(async () =>
         {
             
                 var bands = await BandClientManager.Instance.GetBandsAsync();
                 if (bands != null && bands.Length > 0)
                 {
              
                   var bandClient = await BandClientManager.Instance.ConnectAsync(bands[0]);
                     if (bandClient != null)
                     {
                         MicrosoftBandState.BandClient = bandClient;
                       
                     }
                     MicrosoftBand = new BandController();
                     MicrosoftBand.Name = bands[0].Name;
             }
                 else
                 {
                     var msgDialog = new MessageDialog("You need to connect to this Device with a Microsoft Band through Bluetooth and restart application.", "No Bands Found");
                     await msgDialog.ShowAsync();
                     return;
                 }
                
        
             RaisePropertyChanged(() => MicrosoftBand);
             RaisePropertyChanged(() => Band1Visibility);
             RaisePropertyChanged(() => Band2Visibility);
         });
            });

        }
        private Brush _band2Background;
        public Brush Band2Background
        {
            get { return _band2Background; }
            set { Set(() => Band2Background, ref _band2Background, value); }
        }
        private Brush _band1Background;
        public Brush Band1Background
        {
            get { return _band1Background; }
            set { Set(() => Band1Background, ref _band1Background, value ); }
        }

        private void InitButtons()
        {
            TextBand = SymbolConstants.Band.Text;
            FontFamilyBand = SymbolConstants.Altimeter.FontFamily;
            Band1Background = Band1Color;
            Band2Background = Band2Color;

            RunTextAltimeter = SymbolConstants.Altimeter.Text;
            FontFamilyAltimeter = SymbolConstants.Altimeter.FontFamily;
            BackgroundAltimeter = AltimiterStartColor;

            RunTextAccelerometer = SymbolConstants.Accelerometer.Text;
            FontFamilyAccelerometer = SymbolConstants.Accelerometer.FontFamily;
            BackgroundAccelerometer = AccelerometerStartColor;

            RunTextAmbientLighting = SymbolConstants.AmbientLighting.Text;
            FontFamilyAmbientLighting = SymbolConstants.AmbientLighting.FontFamily;
            BackgroundAmbientLighting = AmbientLightingStartColor;

            RunTextBarometer = SymbolConstants.Barometer.Text;
            FontFamilyBarometer = SymbolConstants.Barometer.FontFamily;
            BackgroundBarometer = BarometerStartColor;

            RunTextBarometer = SymbolConstants.Barometer.Text;
            FontFamilyBarometer = SymbolConstants.Barometer.FontFamily;
            BackgroundBarometer = StartColor;

            RunTextCalories = SymbolConstants.Calories.Text;
            FontFamilyCalories = SymbolConstants.Calories.FontFamily;
            BackgroundCalories = CaloriesStartColor;

            RunTextContact = SymbolConstants.Contact.Text;
            FontFamilyContact = SymbolConstants.Contact.FontFamily;
            BackgroundContact = ContactStartColor;

            RunTextDistance = SymbolConstants.Distance.Text;
            FontFamilyDistance = SymbolConstants.Distance.FontFamily;
            BackgroundDistance = DistanceStartColor;

            RunTextGSR = SymbolConstants.Gsr.Text;
            FontFamilyGSR = SymbolConstants.Gsr.FontFamily;
            BackgroundGSR = GsrStartColor;

            RunTextGyroscope = SymbolConstants.Gyroscope.Text;
            FontFamilyGyroscope = SymbolConstants.Gyroscope.FontFamily;
            BackgroundGyroscope = GyroscopeStartColor;

            RunTextHeartRate = SymbolConstants.HeartRate.Text;
            FontFamilyHeartRate = SymbolConstants.HeartRate.FontFamily;
            BackgroundHeartRate = HeartRateStartColor;

            RunTextRRInterval = SymbolConstants.RRInterval.Text;
            FontFamilyRRInterval = SymbolConstants.RRInterval.FontFamily;
            BackgroundRRInterval = RRInervalStartColor;

            RunTextPedometer = SymbolConstants.Pedometer.Text;
            FontFamilyPedometer = SymbolConstants.Pedometer.FontFamily;
            BackgroundPedometer = PedometerStartColor;

            RunTextSkinTemperature = SymbolConstants.SkinTemperature.Text;
            FontFamilySkinTemperature = SymbolConstants.SkinTemperature.FontFamily;
            BackgroundSkinTemperature = SkinTemperatureStarColor;

            RunTextUVReading = SymbolConstants.RunTextUVReading.Text;
            FontFamilyUVReading = SymbolConstants.RunTextUVReading.FontFamily;
            BackgroundUVReading = UVStartColor;
        }
    }

   
}