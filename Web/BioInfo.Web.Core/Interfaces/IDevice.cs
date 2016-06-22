using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.Core.Interfaces
{
    public interface IDevice
    {
        /// <summary>
        /// In case of band might be domain user.
        /// In case of device in corn field might be device it's self/fieldid/companyid
        /// </summary>
        int OwnerId { get;}

        string Name { get;  }
    }
}
