using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    // Simple API Key authorization attribute
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeader = "X-API-KEY";
        private const string ApiKeyValue = "my-secret-key"; // 🔒 Replace with a strong key

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var extractedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!string.Equals(extractedApiKey, ApiKeyValue))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

    // Model class for calculation history
    public class Calculation
    {
        public int Id { get; set; }
        public string Expression { get; set; } = null!;
        public string Result { get; set; } = null!;
    }

    [ApiController]
    [Route("[controller]")]
    public class CalculatorHistoryController : ControllerBase
    {
        private static List<Calculation> _history = new List<Calculation>();
        private static int _nextId = 1;

        [HttpGet]
        public ActionResult<List<Calculation>> GetHistory()
        {
            return Ok(_history);
        }

        [HttpPost]
        [ApiKeyAuth]
        public ActionResult<Calculation> AddCalculation([FromBody] Calculation calc)
        {
            calc.Id = _nextId++;
            _history.Add(calc);
            return Ok(calc);
        }

        [HttpPut("{id}")]
        [ApiKeyAuth]
        public ActionResult EditCalculation(int id, [FromBody] Calculation calc)
        {
            var existing = _history.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            existing.Expression = calc.Expression;
            existing.Result = calc.Result;
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        [ApiKeyAuth]
        public ActionResult DeleteCalculation(int id)
        {
            var existing = _history.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            _history.Remove(existing);
            return Ok();
        }

        // Public method for other controllers to add history programmatically
        public static Calculation AddToHistory(string expression, string result)
        {
            var calc = new Calculation
            {
                Id = _nextId++,
                Expression = expression,
                Result = result
            };
            _history.Add(calc);
            return calc;
        }
    }
}


