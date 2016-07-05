using BioInfo.Client.ValueConverters;
using Microsoft.Band.Portable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Client.Services
{
    public class BandService : INotifyPropertyChanged
    {
        private BandClient bandClient;
        private BandDeviceInfo band;
        private BandClientManager bandClientManager;

        private string currentSkinTemp = "Not Active";
        public string CurrentSkinTemp
        {
            get { return currentSkinTemp; }
            set
            {
                currentSkinTemp = value;
                OnPropertyChanged();
            }
        }

        private string currentAmbientLight = "Not Active";
        public string CurrentAmbientLight
        {
            get { return currentAmbientLight; }
            set
            {
                currentAmbientLight = value;
                OnPropertyChanged();
            }
        }

        private string currentHeartRate = "Not Active";
        public string CurrentHeartRate
        {
            get { return currentHeartRate; }
            set
            {
                currentHeartRate = value;
                OnPropertyChanged();
            }
        }

        private string currentGSR = "Not Active";
        public string CurrentGSR
        {
            get { return currentGSR; }
            set
            {
                currentGSR = value;
                OnPropertyChanged();
            }
        }

        private string currentUV = "Not Active";
        public string CurrentUV
        {
            get { return currentUV; }
            set
            {
                currentUV = value;
                OnPropertyChanged();
            }
        }

        private string currentPedometer = "Not Active";
        public string CurrentPedometer
        {
            get { return currentPedometer; }
            set
            {
                currentPedometer = value;
                OnPropertyChanged();
            }
        }
        private string currentCalories= "Not Active";
        public string CurrentCalories
        {
            get { return currentCalories; }
            set
            {
                currentCalories = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BandService()
        {
            bandClientManager = BandClientManager.Instance;
        }
        public async Task<IEnumerable<BandDeviceInfo>> getBands()
        {            
            var bands = await bandClientManager.GetPairedBandsAsync();
            return bands;
        }

        public async Task<String> ConnectToBand(BandDeviceInfo b)
        {
            band = b;
            bandClient = await bandClientManager.ConnectAsync(band);
            return String.Format("connected to {0} !", band.Name);            
        }

        public async Task StartReadingSkinTemp()
        {
            await bandClient.SensorManager.SkinTemperature.StartReadingsAsync();
            bandClient.SensorManager.SkinTemperature.ReadingChanged += SkinTemperature_ReadingChanged;
        }
        public async Task StopReadingSkinTemp()
        {
            await bandClient.SensorManager.SkinTemperature.StopReadingsAsync();
            bandClient.SensorManager.SkinTemperature.ReadingChanged -= SkinTemperature_ReadingChanged;
        }
        private async void SkinTemperature_ReadingChanged(object sender, Microsoft.Band.Portable.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Portable.Sensors.BandSkinTemperatureReading> e)
        {
            await Task.Run(() => {
                this.CurrentSkinTemp = CelsiusToFahrenheitConverter.Convert(e.SensorReading.Temperature).ToString();
            });
        }
        
        public async Task StartReadingAmbientLight()
        {
            await bandClient.SensorManager.AmbientLight.StartReadingsAsync();
            bandClient.SensorManager.AmbientLight.ReadingChanged += AmbientLight_ReadingChanged;
        }
        public async Task StopReadingAmbientLight()
        {
            await bandClient.SensorManager.AmbientLight.StopReadingsAsync();
            bandClient.SensorManager.AmbientLight.ReadingChanged -= AmbientLight_ReadingChanged;
        }
        private async void AmbientLight_ReadingChanged(object sender, Microsoft.Band.Portable.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Portable.Sensors.BandAmbientLightReading> e)
        {
            await Task.Run(() =>
            {
                this.CurrentAmbientLight = e.SensorReading.Brightness.ToString();
            });
        }

        public async Task StartReadingHeartRate()
        {
            bool hrConsentGranted;
            switch (bandClient.SensorManager.HeartRate.UserConsented)
            {
                case UserConsent.Declined:
                    hrConsentGranted = false;
                    break;

                case UserConsent.Granted:
                    hrConsentGranted = true;
                    break;

                default:
                case UserConsent.Unspecified:
                    hrConsentGranted = await bandClient.SensorManager.HeartRate.RequestUserConsent();
                    break;
            }
            if (hrConsentGranted)
            {
                await bandClient.SensorManager.HeartRate.StartReadingsAsync();
                bandClient.SensorManager.HeartRate.ReadingChanged += HeartRate_ReadingChanged;
            }
            else
            {
                this.CurrentHeartRate = "No Consent for HR";
            }

        }
        public async Task StopReadingHeartRate()
        {
            await bandClient.SensorManager.HeartRate.StopReadingsAsync();
            bandClient.SensorManager.HeartRate.ReadingChanged -= HeartRate_ReadingChanged;
        }
        private async void HeartRate_ReadingChanged(object sender, Microsoft.Band.Portable.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Portable.Sensors.BandHeartRateReading> e)
        {
            await Task.Run(() =>
            {
                this.CurrentHeartRate = e.SensorReading.HeartRate.ToString();
            });
        }

        public async Task StartReadingGSR()
        {
            await bandClient.SensorManager.Gsr.StartReadingsAsync();
            bandClient.SensorManager.Gsr.ReadingChanged += GSR_ReadingChanged;
        }
        public async Task StopReadingGSR()
        {
            await bandClient.SensorManager.Gsr.StopReadingsAsync();
            bandClient.SensorManager.Gsr.ReadingChanged -= GSR_ReadingChanged;
        }
        private async void GSR_ReadingChanged(object sender, Microsoft.Band.Portable.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Portable.Sensors.BandGsrReading> e)
        {
            await Task.Run(() =>
            {
                this.CurrentGSR = e.SensorReading.Resistance.ToString();
            });
        }
        public async Task StartReadingUV()
        {
            await bandClient.SensorManager.UltravioletLight.StartReadingsAsync();
            bandClient.SensorManager.UltravioletLight.ReadingChanged += UV_ReadingChanged;
        }
        public async Task StopReadingUV()
        {
            await bandClient.SensorManager.UltravioletLight.StopReadingsAsync();
            bandClient.SensorManager.UltravioletLight.ReadingChanged -= UV_ReadingChanged;
        }
        private async void UV_ReadingChanged(object sender, Microsoft.Band.Portable.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Portable.Sensors.BandUltravioletLightReading> e)
        {
            await Task.Run(() =>
            {
                this.CurrentUV = e.SensorReading.Level.ToString();
            });
        }

        public async Task StartReadingPedometer()
        {
            await bandClient.SensorManager.Pedometer.StartReadingsAsync();
            bandClient.SensorManager.Pedometer.ReadingChanged += Pedometer_ReadingChanged;
        }
        public async Task StopReadingPedometer()
        {
            await bandClient.SensorManager.Pedometer.StopReadingsAsync();
            bandClient.SensorManager.Pedometer.ReadingChanged -= Pedometer_ReadingChanged;
        }
        private async void Pedometer_ReadingChanged(object sender, Microsoft.Band.Portable.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Portable.Sensors.BandPedometerReading> e)
        {
            await Task.Run(() =>
            {
                this.CurrentPedometer = e.SensorReading.StepsToday.ToString();
            });
        }

        public async Task StartReadingCalories()
        {
            await bandClient.SensorManager.Calories.StartReadingsAsync();
            bandClient.SensorManager.Calories.ReadingChanged += Calories_ReadingChanged;
        }
        public async Task StopReadingCalories()
        {
            await bandClient.SensorManager.Calories.StopReadingsAsync();
            bandClient.SensorManager.Calories.ReadingChanged -= Calories_ReadingChanged;
        }
        private async void Calories_ReadingChanged(object sender, Microsoft.Band.Portable.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Portable.Sensors.BandCaloriesReading> e)
        {
            await Task.Run(() =>
            {
                this.CurrentCalories = e.SensorReading.CaloriesToday.ToString();
            });
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
