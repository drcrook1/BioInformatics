using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioInfo.Web.ApplicationApi.Controllers;
using Moq;
using BioInfo.Web.Services;
using BioInfo.Web.Core.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using BioInfo.Tests.Helpers;
using BioInfo.Web.Services.Implementations;

namespace BioInfo.Tests.ApplicationApi
{

   

    [TestClass]
    public class DeviceAdminControllerTests
    {
        [TestMethod]
        public async Task Register_device_should_return_200()
        {
            Mock<IRegistrationCoordinator> deviceRegistration = RegisterDevice(() => FunctionResult.Success(true));
            var adminController = new DeviceAdminController(deviceRegistration.Object);

            IHttpActionResult result = await adminController.RegisterDevice(new FakeDevice());

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<bool>));
        }

        [TestMethod]
        public async Task Register_device_should_return_bad_request()
        {
            var ERRORMESSAGE = "error message";
            Mock<IRegistrationCoordinator> deviceRegistration = RegisterDevice(() => FunctionResult.Fail(ERRORMESSAGE));
            var adminController = new DeviceAdminController(deviceRegistration.Object);

            IHttpActionResult result = await adminController.RegisterDevice(new FakeDevice());


            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            var badRequest = result as BadRequestErrorMessageResult;

            Assert.AreEqual(badRequest.Message, ERRORMESSAGE);
        }

        private static Mock<IRegistrationCoordinator> RegisterDevice(Func<FunctionResult<bool>> result)
        {
            var deviceRegistration = new Mock<IRegistrationCoordinator>();
            deviceRegistration.Setup(x => x.RegisterDeviceAsync(It.IsAny<IDevice>())).Returns(Task.FromResult(result()));
            return deviceRegistration;
        }
    }
}
