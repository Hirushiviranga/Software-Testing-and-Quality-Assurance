/*using NUnit.Framework;
using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Tests
{
    public class CalculatorControllerTests
    {
        private CalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CalculatorController();
        }

        [Test]
        public void Add_ReturnsCorrectResult()
        {
            var result = _controller.Add(5, 3) as OkObjectResult;
            Assert.AreEqual(8, result.Value);
        }

        [Test]
        public void Divide_ByZero_ReturnsBadRequest()
        {
            var result = _controller.Divide(5, 0) as BadRequestObjectResult;
            Assert.AreEqual("Cannot divide by zero", result.Value);
        }
    }
}*/
using NUnit.Framework;
using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Tests
{
    [TestFixture]
    public class CalculatorControllerTests
    {
        private CalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CalculatorController();
        }

        [Test]
        public void Add_ReturnsCorrectResult()
        {
            // Act
            var result = _controller.Add(5, 3) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result!.Value, Is.EqualTo(8));
        }

        [Test]
        public void Subtract_ReturnsCorrectResult()
        {
            var result = _controller.Subtract(10, 4) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.That(result!.Value, Is.EqualTo(6));
        }

        [Test]
        public void Multiply_ReturnsCorrectResult()
        {
            var result = _controller.Multiply(2, 5) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.That(result!.Value, Is.EqualTo(10));
        }

        [Test]
        public void Divide_ByNonZero_ReturnsCorrectResult()
        {
            var result = _controller.Divide(10, 2) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.That(result!.Value, Is.EqualTo(5));
        }

        [Test]
        public void Divide_ByZero_ReturnsBadRequest()
        {
            var result = _controller.Divide(10, 0) as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.That(result!.Value, Is.EqualTo("Cannot divide by zero"));
        }
    }
}

