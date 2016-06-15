using BioInfo.Web.Services;
using BioInfo.Web.Services.Implementations;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Web.Http;
using Unity.WebApi;

namespace BioInfo.Web.ApplicationApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            //Get params for injection
            string iotHubConnString = ConfigurationManager.ConnectionStrings["IoTHubConnString"].ConnectionString;
            string domainDBConnString = ConfigurationManager.ConnectionStrings["DomainDBConnString"].ConnectionString;
            string securityDBConnString = ConfigurationManager.ConnectionStrings["SecurityDBConnString"].ConnectionString;
            //Put params in injection constructor
            var baseInjectionConstructor = new InjectionConstructor(iotHubConnString, domainDBConnString, securityDBConnString);
            var iotHubInjectionConstructor = new InjectionConstructor(iotHubConnString);
            //Register Types
            container.RegisterType<IMessagingService, IoTHubService>(iotHubInjectionConstructor);
            
            //Resolve Types
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}