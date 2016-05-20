using Bioinfo.App.Library.Band.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinfo.App.Library.Band.Constants
{
    public static class BandTypeConstants
    {
        public static BandType Band1 = BandType.Band & BandType.Band2;
        public static BandType Band2 = BandType.Band2;
        public static bool CanRun(this BandType bandType, BandType minimumVersion)
        {
            return minimumVersion.HasFlag(bandType);
        }
       
    }
}
