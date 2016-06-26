using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.ApplicationApi.Models.Security.Helpers
{
    public static class AppSettingsHelper
    {
        public static class Email
        {
            public static string EmailAccount
            {
                get { return GetSettingValue("emailAccount"); }
            }

            public static string EmailPassword
            {
                get { return GetSettingValue("emailPassword"); }
            }

            public static string cDyneLicenseKey
            {
                get { return GetSettingValue("cDyneLicenseKey"); }
            }
        }

        public static class Clients
        {
            public static string AdminClientId
            {
                get { return GetSettingValue("adminClientId"); }
            }

            public static string AdminClientUri
            {
                get { return GetSettingValue("adminClientUri"); }
            }

            public static string DomainClientId
            {
                get { return GetSettingValue("domainClientId"); }
            }

            public static string DomainClientUri
            {
                get { return GetSettingValue("domainClientUri"); }
            }
        }

        public static class LoginProviders
        {
            public static class Google
            {
                public static string ClientId
                {
                    get { return GetSettingValue("GoogleClientId"); }
                }
                public static string ClientSecret
                {
                    get { return GetSettingValue("GoogleClientSecret"); }
                }
            }
        }


        public static string GetSettingValue(string settingKey)
        {
            return CloudConfigurationManager.GetSetting(settingKey);
        }

        public static T GetSettingValue<T>(string settingKey) where T : struct
        {
            string settingValue = GetSettingValue(settingKey);

            if (string.IsNullOrEmpty(settingValue))
            {
                return default(T);
            }

            return (T)Convert.ChangeType(settingValue, typeof(T));
        }
    }
}