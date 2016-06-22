using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioInfo.Web.Services.Implementations;
using BioInfo.Web.Services;
using BioInfo.Web.Core.Interfaces;
using System.Threading.Tasks;
using Moq;
using BioInfo.Tests.Helpers;

namespace BioInfo.Tests.Web.Services
{
    [TestClass]
    public class RegistrationCoordinatorTests
    {
        [TestMethod]
        public async Task Should_call_to_Sql_and_ioT_for_registration_when_successful()
        {
            var iotRegistaration = SetupRegistrationForSuccess();
            var sqlRegirstion = SetupRegistrationForSuccess();
            var regCoordinator = new RegistrationCoordinator(iotRegistaration.Object, sqlRegirstion.Object);

            var result = await regCoordinator.RegisterDevice(new FakeDevice());

            Assert.IsFalse(result.DidFail());
            sqlRegirstion.Verify(x => x.RegisterDeviceAsync(It.IsAny<IDevice>()), Times.Once);
            iotRegistaration.Verify(x => x.RegisterDeviceAsync(It.IsAny<IDevice>()), Times.Once);
        }

        [TestMethod]
        public async Task If_SQL_Fails_only_IoT_Isnot_called()
        {
            var iotRegistaration = SetupRegistrationForSuccess();
            var sqlRegirstion = SetupRegistrationForFailure();
            var regCoordinator = new RegistrationCoordinator(iotRegistaration.Object, sqlRegirstion.Object);

            var result = await regCoordinator.RegisterDevice(new FakeDevice());

            Assert.IsTrue(result.DidFail());
            sqlRegirstion.Verify(x => x.RegisterDeviceAsync(It.IsAny<IDevice>()), Times.Once);
            iotRegistaration.Verify(x => x.RegisterDeviceAsync(It.IsAny<IDevice>()), Times.Never);
        }

        [TestMethod]
        public async Task If_IoT_Fails_PartialRegistration()
        {
            var iotRegistaration = SetupRegistrationForFailure();
            var sqlRegirstion = SetupRegistrationForSuccess();
            var regCoordinator = new RegistrationCoordinator(iotRegistaration.Object, sqlRegirstion.Object);

            var result = await regCoordinator.RegisterDevice(new FakeDevice());

            Assert.IsTrue(result.DidFail());
            sqlRegirstion.Verify(x => x.RegisterDeviceAsync(It.IsAny<IDevice>()), Times.Once);
            iotRegistaration.Verify(x => x.RegisterDeviceAsync(It.IsAny<IDevice>()), Times.Once);
        }

        private static Mock<IDeviceRegistration> SetupRegistrationForSuccess()
        {
            var sqlRegirstion = new Mock<IDeviceRegistration>();
            sqlRegirstion.Setup(x => x.RegisterDeviceAsync(It.IsAny<IDevice>())).Returns(() => Task.FromResult(FunctionResult.Success()));
            return sqlRegirstion;
        }

        private static Mock<IDeviceRegistration> SetupRegistrationForFailure()
        {
            var sqlRegirstion = new Mock<IDeviceRegistration>();
            sqlRegirstion.Setup(x => x.RegisterDeviceAsync(It.IsAny<IDevice>())).Returns(() => Task.FromResult(FunctionResult.Fail("failed")));
            return sqlRegirstion;
        }
    }


    public class SuccessRegistrationlFake : IDeviceRegistration
    {
        public Task<FunctionResult<bool>> RegisterDeviceAsync(IDevice device)
        {
            return Task.FromResult(FunctionResult.Success());
        }

        public Task<FunctionResult<bool>> UnregisterDeviceAsync(IDevice device)
        {
            return Task.FromResult(FunctionResult.Success());
        }
    }
}
