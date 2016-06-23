using BioInfo.Web.Core.DTOs;
using BioInfo.Web.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Tests.Helpers
{
    public class FakeDevice : IoTHubDevice
    {
        public FakeDevice() : base("FakeDevice", 1)
        {

        }

    }
}
