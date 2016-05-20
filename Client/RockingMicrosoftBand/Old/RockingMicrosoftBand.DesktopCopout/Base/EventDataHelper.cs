using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockingMicrosoftBand.DesktopCopout.Base
{
    public static class EventDataHelper
    {
        public static EventData ToEventData(this string message)
        {
            return new EventData(Encoding.UTF8.GetBytes(message));
        }
    }
}
