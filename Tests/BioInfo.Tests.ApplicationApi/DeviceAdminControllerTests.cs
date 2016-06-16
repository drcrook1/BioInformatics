using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioInfo.Web.ApplicationApi.Controllers;
using Moq;
using BioInfo.Web.Services;
using BioInfo.Web.Core.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace BioInfo.Tests.ApplicationApi
{

    public class Device : IDevice
    {
        public FunctionResult<string> GetName()
        {
            return new FunctionResult<string>("FakeDevice");
        }
    }

    [TestClass]
    public class DeviceAdminControllerTests
    {
        [TestMethod]
        public async Task Register_device_should_return_200()
        {
            var deviceRegistration = new Mock<IDeviceRegistration>();
            deviceRegistration.Setup(x => x.RegisterDeviceAsync(It.IsAny<IDevice>())).Returns(Task.FromResult(new FunctionResult<bool>(true)));

            var adminController = new DeviceAdminController(deviceRegistration.Object);

            IHttpActionResult result = await adminController.RegisterDevice(new Device());

    
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<bool>));
        }

        [TestMethod]
        public async Task Register_device_should_return_bad_request()
        {
            var deviceRegistration = new Mock<IDeviceRegistration>();
            deviceRegistration.Setup(x => x.RegisterDeviceAsync(It.IsAny<IDevice>())).Returns(Task.FromResult(new FunctionResult<bool>(false, "error message",true)));

            var adminController = new DeviceAdminController(deviceRegistration.Object);

            IHttpActionResult result = await adminController.RegisterDevice(new Device());


            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            var badRequest = result as BadRequestErrorMessageResult;

            Assert.AreEqual(badRequest.Message, "error message");
        }
    }
}
