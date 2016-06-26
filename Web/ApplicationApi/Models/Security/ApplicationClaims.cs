using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.ApplicationApi.Models.Security
{
    public static class ApplicationClaims
    {
        public static string Realm = "foosyeRealm";
    }

    public static class RealmTypes
    {
        public static string Trade = "trade";
        public static string Event = "event";
        public static string Public = "public";
    }
}