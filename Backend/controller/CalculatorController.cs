//non automated tests
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        public CalculatorController()
        {
        }

        // Add two numbers
        [HttpGet("Add")]
        public IActionResult Add(int a, int b)
        {
            return Ok(a + b);
        }

        // Subtract two numbers
        [HttpGet("Subtract")]
        public IActionResult Subtract(int a, int b)
        {
            return Ok(a - b);
        }

        // Multiply two numbers
        [HttpGet("Multiply")]
        public IActionResult Multiply(int a, int b)
        {
            return Ok(a * b);
        }

        // Divide two numbers
        [HttpGet("Divide")]
        public IActionResult Divide(int a, int b)
        {
            if (b == 0)
                return BadRequest("Cannot divide by zero");

            return Ok(a / b);
        }
    }
}

