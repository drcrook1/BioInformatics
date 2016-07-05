using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BioInfo.Web.ApplicationApi.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Get()
        {
            return Ok("It Works!");
        }
    }
}
