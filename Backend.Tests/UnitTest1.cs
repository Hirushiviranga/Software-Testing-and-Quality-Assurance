using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Backend.Controllers;

namespace Backend.Tests
{
    [TestFixture]
    public class CalculatorUnitTests
    {
        private CalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CalculatorController();
        }

        [Test]
        public void Add_Test()
        {
            var result = _controller.Add(2, 3) as OkObjectResult;
            Assert.AreEqual(5, result?.Value);
        }

        [Test]
        public void Subtract_Test()
        {
            var result = _controller.Subtract(5, 3) as OkObjectResult;
            Assert.AreEqual(2, result?.Value);
        }

        [Test]
        public void Multiply_Test()
        {
            var result = _controller.Multiply(4, 5) as OkObjectResult;
            Assert.AreEqual(20, result?.Value);
        }

        [Test]
        public void Divide_Test()
        {
            var result = _controller.Divide(10, 2) as OkObjectResult;
            Assert.AreEqual(5, result?.Value);
        }

        [Test]
        public void DivideByZero_ReturnsBadRequest()
        {
            var result = _controller.Divide(10, 0) as BadRequestObjectResult;
            Assert.AreEqual(400, result?.StatusCode);
        }
    }
}
