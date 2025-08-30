using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
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
        public ActionResult<Calculation> AddCalculation([FromBody] Calculation calc)
        {
            calc.Id = _nextId++;
            _history.Add(calc);
            return Ok(calc);
        }

        [HttpPut("{id}")]
        public ActionResult EditCalculation(int id, [FromBody] Calculation calc)
        {
            var existing = _history.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            existing.Expression = calc.Expression;
            existing.Result = calc.Result;
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCalculation(int id)
        {
            var existing = _history.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            _history.Remove(existing);
            return Ok();
        }
    }
}
