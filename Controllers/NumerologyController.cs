using Microsoft.AspNetCore.Mvc;
using NumerologyApi.Models;
using NumerologyApi.Services;

namespace NumerologyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumerologyController : ControllerBase
    {
        private readonly INumerologyService _service;

        public NumerologyController(INumerologyService service)
        {
            _service = service;
        }

        [HttpPost("analyze")]
        public ActionResult<NumerologyResult> Analyze([FromBody] NumerologyRequest request)
        {
            var result = _service.Analyze(request);
            return Ok(result);
        }


        [HttpGet("vedic-grid")]
        public ActionResult<VedicGridResponse> GetVedicGrid([FromQuery] string mobile)
       => Ok(_service.GetVedicGridAnalysis(mobile));
    }
}
