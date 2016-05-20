using Microsoft.Band;
using Microsoft.Band.Sensors;
using Bioinfo.App.Library.Band.Enum;
using Bioinfo.App.Library.Band.IBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bioinfo.App.Library.Band.Base
{
    public static class SensorFactory
    {
        public static SensorController<U> BuildSensorController<T, U>(string name, T sensor, Action<U> onRead) where T : IBandSensor<U> where U : IBandSensorReading
        {
            return new SensorController<U>(name, sensor, onRead);
        } 
    }
}