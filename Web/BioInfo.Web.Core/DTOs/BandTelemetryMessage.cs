using BioInfo.Web.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BioInfo.Web.Core.DTOs
{
    //GalvSkinRes	Accelerometer	Subject Medications	Subject MedicalHistory
    public class BandTelemetryMessage : IMessage
    {
        public int ExperimentId { get; set; }
        public int BandId { get; set; }
        public DateTime CollectionTime { get; set; }          
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float BarometricPressure { get; set; }
        public float Capacitance { get; set; }
        public float HeartRate { get; set; }
        public float AmbientLight { get; set; }
        public float SkinTemperature { get; set; }
        public float UV { get; set; }
        public float GalvSkinRes { get; set; }
        public float Accelerometer { get; set; }
        public float Microphone { get; set; }

        public string JsonSerialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public FunctionResult<IMessage> JsonDeSerialize(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<BandTelemetryMessage>(message);
                return new FunctionResult<IMessage>(result);
            }
            catch(Exception e)
            {
                return new FunctionResult<IMessage>(null, e.Message, true);
            }
        }
    }
}
