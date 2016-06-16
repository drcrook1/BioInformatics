using BioInfo.Web.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Tests.ApplicationApi.TestHelpers
{
    public class FakeDevice : IDevice
    {
        public FunctionResult<string> GetName()
        {
            return new FunctionResult<string>("FakeDevice");
        }
    }
}
