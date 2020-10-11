using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AFIExercise.API.Models;
using FluentAssertions;
using Xunit;

namespace AFIExercise.Tests.Integration
{
    public class ApiIntegrationTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServer;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public ApiIntegrationTests(TestServerFixture testServer)
        {
            _testServer = testServer;
        }


        [Fact]
        public async Task ProperlyPopulatedCustomerRegistrationRequestReturns200AndCustomerRegistrationCreatedObject()
        {
            var request = new CustomerRegistrationRequest
            {
                FirstName = "Bob",
                Surname = "Smith",
                DateOfBirth = new DateTime(2000, 1, 1),
                EmailAddress = "robert@smith.co.uk",
                PolicyNumber = "AB-123456"
            };

            var response = await PostCustomerRegistrationRequest(request);

            response.StatusCode.Should().Be((int) HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonSerializer.Deserialize<CustomerRegistrationCreated>(responseBody, _jsonOptions);

            responseObject.CustomerId.Should().BeGreaterThan(0);
        }

        private async Task<HttpResponseMessage> PostCustomerRegistrationRequest(CustomerRegistrationRequest request)
        {
            return await _testServer.Client.PostAsync("/api/registration",
                new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                    "application/json"));
        }

        [Fact]
        public async Task InvalidCustomerRegistrationRequestReturns400AndValidationMessageObjects()
        {
            var request = new CustomerRegistrationRequest();

            var response = await PostCustomerRegistrationRequest(request);

            response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonSerializer.Deserialize<ValidationMessage[]>(responseBody, _jsonOptions);

            responseObject[0].Property.Should().Be("FirstName");
            responseObject[0].Message.Should().Be("'First Name' must not be empty.");

            responseObject[1].Property.Should().Be("Surname");
            responseObject[1].Message.Should().Be("'Surname' must not be empty.");

            responseObject[2].Property.Should().Be("PolicyNumber");
            responseObject[2].Message.Should().Be("'Policy Number' must not be empty.");

            responseObject[3].Property.Should().Be("");
            responseObject[3].Message.Should().Be("'Date Of Birth' or 'Email Address' must not be empty.");
        }
    }

}
