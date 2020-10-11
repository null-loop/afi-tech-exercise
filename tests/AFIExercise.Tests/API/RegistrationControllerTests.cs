using System;
using System.Threading.Tasks;
using AFIExercise.API.Controllers;
using AFIExercise.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Models = AFIExercise.API.Models;
using Moq;
using Xunit;
using ServiceExtensions = AFIExercise.API.ServiceExtensions;

namespace AFIExercise.Tests.API
{
    public class RegistrationControllerTests
    {
        [Fact]
        public async Task SubmitCustomerRegistrationReturnsSuccessfulRegistrationAsOkObjectResultWithCustomerRegistrationCreatedObject()
        {
            var registrationServiceMock = new Mock<AFIExercise.Services.ICustomerRegistrationService>();
            var request = new Models.CustomerRegistrationRequest
            {
                FirstName = "Bob",
                Surname = "Smith",
                DateOfBirth = new DateTime(2000, 1, 1),
                EmailAddress = "robert@smith.co.uk",
                PolicyNumber = "AB-123456"
            };

            registrationServiceMock.Setup(a => a.Register(It.Is<AFIExercise.Services.CustomerRegistrationRequest>(
                    registration => registration.FirstName == request.FirstName &&
                                    registration.Surname == request.Surname &&
                                    registration.DateOfBirth == request.DateOfBirth &&
                                    registration.EmailAddress == request.EmailAddress &&
                                    registration.PolicyNumber == request.PolicyNumber)))
                .ReturnsAsync(new AFIExercise.Services.CustomerRegistrationResult(101));

            var controller = new RegistrationController(registrationServiceMock.Object, ServiceExtensions.CreateMapper());

            var result = await controller.SubmitCustomerRegistration(request);

            var okObjectResult = result as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            var customerRegistrationCreated = okObjectResult.Value as Models.CustomerRegistrationCreated;

            customerRegistrationCreated.Should().NotBeNull();

            customerRegistrationCreated.CustomerId.Should().Be(101);
        }

        [Fact]
        public async Task SubmitCustomerRegistrationReturnsInvalidationRegistrationAsBadRequestObjectResultWithValidationMessageObjects()
        {
            var registrationServiceMock = new Mock<AFIExercise.Services.ICustomerRegistrationService>();
            var request = new Models.CustomerRegistrationRequest();

            registrationServiceMock.Setup(a => a.Register(It.Is<AFIExercise.Services.CustomerRegistrationRequest>(
                    registration => registration.FirstName == request.FirstName &&
                                    registration.Surname == request.Surname &&
                                    registration.DateOfBirth == request.DateOfBirth &&
                                    registration.EmailAddress == request.EmailAddress &&
                                    registration.PolicyNumber == request.PolicyNumber)))
                .ReturnsAsync(new AFIExercise.Services.CustomerRegistrationResult(
                    new ValidationMessage("FirstName", "'First Name' must not be empty."),
                    new ValidationMessage("Surname", "'Surname' must not be empty."),
                    new ValidationMessage("PolicyNumber", "'Policy Number' must not be empty."),
                    new ValidationMessage("", "'Date Of Birth' or 'Email Address' must not be empty.")));

            var controller = new RegistrationController(registrationServiceMock.Object, ServiceExtensions.CreateMapper());

            var result = await controller.SubmitCustomerRegistration(request);

            var badRequestObjectResult = result as BadRequestObjectResult;

            badRequestObjectResult.Should().NotBeNull();
            var validationMessages = badRequestObjectResult.Value as Models.ValidationMessage[];

            validationMessages.Should().NotBeNull();

            validationMessages.Should().HaveCount(4);

            validationMessages[0].Property.Should().Be("FirstName");
            validationMessages[0].Message.Should().Be("'First Name' must not be empty.");

            validationMessages[1].Property.Should().Be("Surname");
            validationMessages[1].Message.Should().Be("'Surname' must not be empty.");

            validationMessages[2].Property.Should().Be("PolicyNumber");
            validationMessages[2].Message.Should().Be("'Policy Number' must not be empty.");

            validationMessages[3].Property.Should().Be("");
            validationMessages[3].Message.Should().Be("'Date Of Birth' or 'Email Address' must not be empty.");
        }
    }
}
