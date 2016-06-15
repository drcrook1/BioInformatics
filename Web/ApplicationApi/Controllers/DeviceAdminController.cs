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
        IMessagingService MessagingService = null;

        public DeviceAdminController(IMessagingService messagingService)
        {
            this.MessagingService = messagingService;
        }

        [HttpPost]
        [Route("RegisterDevice")]
        public async Task<IHttpActionResult> RegisterDevice([FromBody]IDevice device)
        {
            var result = await MessagingService.RegisterDeviceAsync(device);
            if (result.DidFail())
                return BadRequest(result.GetFriendlyError());
            return Ok(result.GetResult());
        }

        [HttpPost]
        [Route("UnregisterDevice")]
        public async Task<IHttpActionResult> UnregisterDevice([FromBody]IDevice device)
        {
            var result = await MessagingService.UnregisterDeviceAsync(device);
            if (result.DidFail())
                return BadRequest(result.GetFriendlyError());
            return Ok(result.GetResult());
        }
    }
}
