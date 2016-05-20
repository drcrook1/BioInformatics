using Microsoft.Band;
using Microsoft.Band.Sensors;
using RockingMicrosoftBand.Band.Enum;
using RockingMicrosoftBand.Band.IBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Core;

namespace RockingMicrosoftBand.Band.Base
{
    public static class SensorFactory
    {
        public static SensorController<U> BuildSensorController<T, U>(string name, T sensor, Action<U> onRead) where T : IBandSensor<U> where U : IBandSensorReading
        {
            return new SensorController<U>(name, sensor, onRead);
        } 
    }
}