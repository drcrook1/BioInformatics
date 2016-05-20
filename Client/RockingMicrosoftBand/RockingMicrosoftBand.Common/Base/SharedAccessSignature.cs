using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockingMicrosoftBand.Common.Base
{
    public static class SharedAccessSignatureHelper
    {
        public static string CreateSASConnectionString(string nameSpace, string hubName, string publisher, SasMode mode, string keyName, string key)
        {

            var initialString = string.Empty;
            switch (mode)
            {
               
                case SasMode.Aqmp:
                    string.Format("SharedAccessSignature sr=sb://{0}.servicebus.windows.net/{1}/publishers/{3}&sig=edOnu733Cnd8LPQyGFRMLtd/GEqe8xcuf0uGEvhq4Z4=&se=1453088792&skn=RootManageSharedAccessKey",
                       nameSpace, hubName, publisher); 
                    break;
                case SasMode.Http:
                    string.Format("SharedAccessSignature sr=https://{0}.servicebus.windows.net/{1}/publishers/{3}/messages&sig=I6RYNgk4X6e0tRS7C0yMvsTRxNpGDcMJuxZ1C2vEKXk=&se=1453085445&skn=RootManageSharedAccessKey",
                       nameSpace, hubName, publisher);
                    break;
                default:
                    break;
            }
            var httpEncoded = System.Net.WebUtility.UrlEncode(initialString);
            return httpEncoded;
        }
    }
}
