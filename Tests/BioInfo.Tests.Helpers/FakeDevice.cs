using BioInfo.Web.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Tests.Helpers
{
    public class FakeDevice : IDevice
    {
        public int DomainUserId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return "FakeDevice";
            }
        }

    }
}
