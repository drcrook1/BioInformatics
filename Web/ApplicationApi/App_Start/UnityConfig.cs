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
            var iotHubInjectionConstructor = new InjectionConstructor(iotHubConnString);
            var databaseInjection = new InjectionConstructor(domainDBConnString);


            //Register IotHub service with all interfaces
            container.RegisterType<IoTHubService, IoTHubService>(iotHubInjectionConstructor);

            var iohub = container.Resolve<IoTHubService>();
            container.RegisterInstance<IMessagingService>(iohub);
            container.RegisterInstance<IDeviceRegistration>("iothub",iohub);

            //set up sql registration
            container.RegisterType<IDeviceRegistration, SqlDeviceRegistration>("sql", databaseInjection);

            //set up coordinator
            var coordinatorConstructor = new InjectionConstructor(iohub, container.Resolve<IDeviceRegistration>("sql"));
            container.RegisterType<IRegistrationCoordinator, SimpleRegistrationCoordinator>(coordinatorConstructor);


            //Resolve Types
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}