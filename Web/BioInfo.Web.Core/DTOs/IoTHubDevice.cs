using BioInfo.Web.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.Core.DTOs
{
    public class IoTHubDevice : IDevice, IMessage
    {
        private string _name;
        private int _domainUserId;

        public int DomainUserId
        {
            get
            {
                return _domainUserId;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public IoTHubDevice(string name, int domainUserId)
        {
            this._name = name;
            this._domainUserId = domainUserId;
        }

    

        public string JsonSerialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public FunctionResult<IMessage> JsonDeSerialize(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<IoTHubDevice>(message);
                return new FunctionResult<IMessage>(result);
            }
            catch (Exception e)
            {
                return new FunctionResult<IMessage>(null, e.Message, true);
            }
        }
    }
}
