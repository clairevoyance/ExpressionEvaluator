using ExpressionEvaluator.Api.Models;
using ExpressionEvaluator.Api.Data;
using ExpressionEvaluator.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpressionEvaluator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {

        private readonly ExpressionDbContext _db;
        private readonly ExpressionEvaluatorService _evaluator;

        public EvaluationController(ExpressionDbContext db, ExpressionEvaluatorService evaluator)
        {
            _db = db;
            _evaluator = evaluator;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] AddRequest request)
        {
            int result = request.A + request.B;
            return Ok(new { Result = result });
        }

        [HttpPost("evaluate")]
        public IActionResult Evaluate([FromBody] ExpressionRequest request)
        {
            try
            {
                var result = _evaluator.Evaluate(request.Expression);

                var record = new ExpressionRecord {
                    Expression = request.Expression,
                    Result = result.ToString()
                };

                _db.Expressions.Add(record);
                _db.SaveChanges();
                
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("history")]
        public IActionResult History([FromQuery] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return BadRequest("Result value cannot be empty.");

            var sql = "SELECT * FROM Expressions WHERE Result = {0} ORDER BY CreatedAt DESC";

            var results = _db.Expressions
                            .FromSqlRaw(sql, value)
                            .ToList();

            return Ok(results);
        }
    }
}
