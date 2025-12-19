using ExpressionEvaluator.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpressionEvaluator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add([FromBody] AddRequest request)
        {
            int result = request.A + request.B;
            return Ok(new { Result = result });
        }
    }
}
