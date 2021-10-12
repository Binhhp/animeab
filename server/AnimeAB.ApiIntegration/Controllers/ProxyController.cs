using Microsoft.AspNetCore.Mvc;

namespace AnimeAB.Core.Apis
{
    [Route("proxy")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        [HttpGet]
        [Route("{url}")]
        public IActionResult ProxyCors([FromRoute]string url)
        {
            return Ok();
        }
    }
}
