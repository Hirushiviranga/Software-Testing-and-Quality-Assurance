//automated tests

using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        public CalculatorController() { }

        [HttpGet("Add")]
        public IActionResult Add(int a, int b)
        {
            int result = a + b;
            string expression = $"{a} + {b}";
            CalculatorHistoryController.AddToHistory(expression, result.ToString());
            return Ok(result);
        }

        [HttpGet("Subtract")]
        public IActionResult Subtract(int a, int b)
        {
            int result = a - b;
            string expression = $"{a} - {b}";
            CalculatorHistoryController.AddToHistory(expression, result.ToString());
            return Ok(result);
        }

        [HttpGet("Multiply")]
        public IActionResult Multiply(int a, int b)
        {
            int result = a * b;
            string expression = $"{a} * {b}";
            CalculatorHistoryController.AddToHistory(expression, result.ToString());
            return Ok(result);
        }

        [HttpGet("Divide")]
        public IActionResult Divide(int a, int b)
        {
            if (b == 0)
                return BadRequest("Cannot divide by zero");

            int result = a / b;
            string expression = $"{a} / {b}";
            CalculatorHistoryController.AddToHistory(expression, result.ToString());
            return Ok(result);
        }
    }
}
