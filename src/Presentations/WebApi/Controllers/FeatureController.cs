using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        [HttpGet("/enabled")]
        [FeatureGate(Features.EnabledExample)]
        public IActionResult Enabled()
        {
            return Ok();
        }

        [HttpGet("/disabled")]
        [FeatureGate(Features.DisabledExample)]
        public IActionResult Disabled()
        {
            return Ok();
        }
    }
}