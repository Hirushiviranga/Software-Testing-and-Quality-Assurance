//pasing test case without refactoring
/*using NUnit.Framework;
using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private CalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CalculatorController();
        }

        [Test]
        public void Add_WhenGivenTwoNumbers_ReturnsCorrectSum()
        {
            int a = 5, b = 3;
            var result = _controller.Add(a, b) as OkObjectResult;

            Assert.That(result, Is.Not.Null); // ✅ correct NUnit syntax
            Assert.That(result.Value, Is.EqualTo(8));
        }

        [Test]
        public void Divide_WhenDivideByZero_ReturnsBadRequest()
        {
            int a = 10, b = 0;
            var result = _controller.Divide(a, b);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}*/

//passing test case with refactoring
using NUnit.Framework;
using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Tests
{
    [TestFixture]
    public class CalculatorAddTests
    {
        private CalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CalculatorController();
        }

        // Refactored Add tests using TestCase
        [TestCase(5, 3, 8)]
        [TestCase(10, -2, 8)]
        [TestCase(0, 0, 0)]
        public void Add_WhenGivenTwoNumbers_ReturnsCorrectSum(int a, int b, int expected)
        {
            var result = _controller.Add(a, b) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(expected));
        }
    }
}


//failing testcase without refactoring
/*using NUnit.Framework;
using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Tests
{
    [TestFixture]
    public class CalculatorAddFailingTest
    {
        private CalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CalculatorController();
        }

        [Test]
        public void Add_FailingTest()
        {
            var result = _controller.Add(2, 2) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(5)); // ❌ Deliberately wrong
        }
    }
}*/

//failing testcase with refactoring
/*using NUnit.Framework;
using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private CalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CalculatorController();
        }

        // Refactored Add test using TestCase
        [TestCase(5, 3, 8)]
        [TestCase(10, -2, 8)]
        [TestCase(0, 0, 0)]
        public void Add_WhenGivenTwoNumbers_ReturnsCorrectSum(int a, int b, int expected)
        {
            var result = _controller.Add(a, b) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(expected));
        }

        //  Divide by zero test
        [Test]
        public void Divide_WhenDivideByZero_ReturnsBadRequest()
        {
            var result = _controller.Divide(10, 0);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        //  Failing test: deliberately expecting wrong result
        [Test]
        public void Add_FailingTest()
        {
            var result = _controller.Add(2, 2) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(5)); //  2 + 2 != 5 → this will fail
        }
    }
}
*/
