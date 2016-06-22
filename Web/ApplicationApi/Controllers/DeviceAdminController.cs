using BioInfo.Web.Core.Interfaces;
using BioInfo.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BioInfo.Web.ApplicationApi.Controllers
{
    [RoutePrefix("api/DeviceAdmin")]
    public class DeviceAdminController : ApiController
    {
        IDeviceRegistration registrationService = null;

        public DeviceAdminController(IDeviceRegistration registrationService)
        {
            this.registrationService = registrationService;
        }

        [HttpPost]
        [Route("RegisterDevice")]
        public async Task<IHttpActionResult> RegisterDevice([FromBody]IDevice device)
        {
            var result = await registrationService.RegisterDeviceAsync(device);
            if (result.DidFail())
                return BadRequest(result.GetFriendlyError());
            return Ok(result.GetResult());
        }

        [HttpPost]
        [Route("UnregisterDevice")]
        public async Task<IHttpActionResult> UnregisterDevice([FromBody]IDevice device)
        {
            var result = await registrationService.UnregisterDeviceAsync(device);
            if (result.DidFail())
                return BadRequest(result.GetFriendlyError());
            return Ok(result.GetResult());
        }
    }
}
