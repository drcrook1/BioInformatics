using BioInfo.Web.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.Core.DTOs
{
    public class ExperimentMessage : IMessage
    {
        public int ExperimentId { get; set; }
        public int BandId { get; set; }
        public string SubjectFirstName { get; set; }
        public string SubjectLastName { get; set; }
        public DateTime SubjectBirthDate { get; set; }
        public string SubjectGender { get; set; }
        public float SubjectHeightInches { get; set; }
        public float SubjectWeightLbs { get; set; }
        public string SubjectRace { get; set; }
        public string SubjectMedications { get; set; }
        public string SubjectMedicalHistory { get; set; }

        public string JsonSerialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public FunctionResult<IMessage> JsonDeSerialize(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<ExperimentMessage>(message);
                return new FunctionResult<IMessage>(result);
            }
            catch (Exception e)
            {
                return new FunctionResult<IMessage>(null, e.Message, true);
            }
        }
    }
}
