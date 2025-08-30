using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Backend.Tests
{
    [TestFixture]
    public class CalculatorApiTests
    {
        private HttpClient _client = null!; // Non-nullable after setup
        private const string BaseUrl = "http://localhost:5095/Calculator";

        [OneTimeSetUp]
        public void Setup()
        {
            _client = new HttpClient();
        }

        [Test]
        public async Task AddEndpoint_ReturnsCorrectResult()
        {
            int a = 5;
            int b = 3;

            string url = $"{BaseUrl}/Add?a={a}&b={b}";
            HttpResponseMessage response = await _client.GetAsync(url);

            // Validate HTTP status code
            Assert.That(response.IsSuccessStatusCode, Is.True, "Add endpoint returned error status");

            string result = await response.Content.ReadAsStringAsync();

            // Validate payload
            Assert.That(result, Is.EqualTo("8"), "Add endpoint returned incorrect result");
        }

        [Test]
        public async Task SubtractEndpoint_ReturnsCorrectResult()
        {
            int a = 10;
            int b = 4;

            string url = $"{BaseUrl}/Subtract?a={a}&b={b}";
            HttpResponseMessage response = await _client.GetAsync(url);

            // Validate HTTP status code
            Assert.That(response.IsSuccessStatusCode, Is.True, "Subtract endpoint returned error status");

            string result = await response.Content.ReadAsStringAsync();

            // Validate payload
            Assert.That(result, Is.EqualTo("6"), "Subtract endpoint returned incorrect result");
        }

        [Test]
        public async Task AddEndpoint_InvalidInput_ReturnsBadRequest()
        {
            string url = $"{BaseUrl}/Add?a=abc&b=5";
            HttpResponseMessage response = await _client.GetAsync(url);

            // Expect 400 Bad Request for invalid input
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
    }
}
