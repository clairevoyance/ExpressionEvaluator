using ExpressionEvaluator.Api.Models;
using ExpressionEvaluator.Api.Services;
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

        [HttpPost("evaluate")]
        public IActionResult Evaluate([FromBody] ExpressionRequest request, [FromServices] ExpressionEvaluatorService evaluator)
        {
            try
            {
                var result = evaluator.Evaluate(request.Expression);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
