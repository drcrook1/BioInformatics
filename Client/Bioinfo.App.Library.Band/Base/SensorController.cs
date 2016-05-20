using GalaSoft.MvvmLight;
using Microsoft.Band;
using Bioinfo.App.Library.Band.Constants;
using Bioinfo.App.Library.Band.Enum;
using Bioinfo.App.Library.Band.IBase;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Band.Sensors;
using BioInfo.App.Library.Common.Helpers;
using BioInfo.App.Library.Common;

namespace Bioinfo.App.Library.Band.Base
{
    public class SensorController<T> : ViewModelBase where T : IBandSensorReading
    {
        public SensorController(string name, IBandSensor<T> sensor, Action<T> action)
        {
            Name = name;
            Sensor = sensor;
            HasConsent = false;
            Readings = new ObservableCollection<IAllBandSensorReadings>();
            OnRead = action;
            Errors = string.Empty;
            LocalOnRead = (args) => 
            {
                TriggerOnRead((T)args);
            };
            GetConsent();
            LatestStatus = "Not yet activated.";
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

        private bool _isAvailable;
        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { Set(() => IsAvailable, ref _isAvailable, value); }
        }

        public async Task Init()
        {
            Sensor.ReadingChanged -= (async (s, args) => await InvocationHelper.ExecuteWithIgnoreException(async () =>
            {
                 LocalOnRead.Invoke((T)args.SensorReading);
                 OnRead.Invoke((T)args.SensorReading);
            }));
            Sensor.ReadingChanged += (async (s, args) => await InvocationHelper.ExecuteWithIgnoreException(async () =>
            {
                LocalOnRead.Invoke((T)args.SensorReading);
                OnRead.Invoke((T)args.SensorReading);
            }));
        }
        private string _lateststatus;
        public string LatestStatus
        {
            get { return _lateststatus; }
            private set { Set(() => LatestStatus, ref _lateststatus, value); }
        }
     
        private Action<T> _onRead;
        public Action<T> OnRead
        {
            get
            {
                return _onRead;
            }
            set
            {
                _onRead = value;
                Init();
            }
        }
        private Action<T> _localOnRead;
        public Action<T> LocalOnRead
        {
            get
            {
                return _localOnRead;
            }
            set
            {
                _localOnRead = value;
                Init();
            }
        }
        private async Task TriggerOnRead(T reading)
        {
            IAllBandSensorReadings anyReading = null;
            anyReading = new AnyReading().PopulateFromSensorReading(reading);
            var status = anyReading.Status;
            await ApplicationState.AnyDispatcher.Invoke( () =>
            {
                LatestStatus = status;
                if (anyReading != null)
                {
                    Readings.Add(anyReading);
                }
            });

        }
        private IBandSensor<T> _actualSensor;
        private bool _hasConsent;
        private ObservableCollection<IAllBandSensorReadings> _readings;
        private BandSensorType _type;
      
        public IBandSensor<T> Sensor
        {
            get { return _actualSensor; }
            set { Set(() => Sensor, ref _actualSensor, value); }
        }

        public bool HasConsent
        {
            get { return _hasConsent; }
            set { Set(() => HasConsent, ref _hasConsent, value); }
        }
        public ObservableCollection<IAllBandSensorReadings> Readings
        {
            get { return _readings; }
            set { Set(() => Readings, ref _readings, value); }
        }
        public async Task<bool> GetConsent()
        {
            await ApplicationState.AnyDispatcher.Invoke(async () =>
            {
                 HasConsent = HasConsent || Sensor.GetCurrentUserConsent() == UserConsent.Granted || await Sensor.RequestUserConsentAsync();
            });
            return HasConsent;
           
       }

        public async Task StartReading()
        {
            if (await GetConsent())
            {
                await Sensor.StartReadingsAsync();
            }
        }
        public async Task StopReading()
        {
            await Sensor.StopReadingsAsync();
            LatestStatus = "Stopped";
        }

      
        private string _errors;
        public string Errors
        {
            get { return _errors; }
            set { Set(() => Errors, ref _errors, value); }
        }
       
    }
}