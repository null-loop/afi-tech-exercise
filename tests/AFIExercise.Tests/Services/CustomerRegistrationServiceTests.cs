using System;
using System.Threading.Tasks;
using AFIExercise.Data;
using AFIExercise.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace AFIExercise.Tests.Services
{
    public class CustomerRegistrationServiceTests
    {
        [Fact]
        public async Task InvalidCustomerRegistrationRequestReturnsErrors()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new CustomerRegistrationService(unitOfWorkMock.Object, new CustomerRegistrationRequestValidator());
            var request = new CustomerRegistrationRequest();
            var result = await service.Register(request);

            result.IsSuccessful.Should().BeFalse();
            result.CustomerId.Should().BeNull();
            result.ValidationMessages.Length.Should().Be(4);

            result.ValidationMessages[0].Property.Should().Be("FirstName");
            result.ValidationMessages[0].Message.Should().Be("'First Name' must not be empty.");

            result.ValidationMessages[1].Property.Should().Be("Surname");
            result.ValidationMessages[1].Message.Should().Be("'Surname' must not be empty.");

            result.ValidationMessages[2].Property.Should().Be("PolicyNumber");
            result.ValidationMessages[2].Message.Should().Be("'Policy Number' must not be empty.");

            result.ValidationMessages[3].Property.Should().Be("");
            result.ValidationMessages[3].Message.Should().Be("'Date Of Birth' or 'Email Address' must not be empty.");
        }

        [Fact]
        public async Task ValidCustomerRegistrationCallsUnitOfWorkAndReturnsCorrectCustomerId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var request = new CustomerRegistrationRequest
            {
                FirstName = "Bob",
                Surname = "Smith",
                DateOfBirth = new DateTime(2000, 1, 1),
                EmailAddress = "robert@smith.co.uk",
                PolicyNumber = "AB-123456"
            };

            unitOfWorkMock.Setup(a => a.CustomerCustomerRegistrations.Add(It.Is<CustomerRegistration>(
                registration => registration.FirstName == request.FirstName && registration.Surname == request.Surname &&
                     registration.DateOfBirth == request.DateOfBirth && registration.EmailAddress == request.EmailAddress &&
                     registration.PolicyNumber == request.PolicyNumber)))
                .Callback<CustomerRegistration>(r=>r.Id = 101);

            var service = new CustomerRegistrationService(unitOfWorkMock.Object, new CustomerRegistrationRequestValidator());
            
            var result = await service.Register(request);

            result.IsSuccessful.Should().BeTrue();
            result.CustomerId.Should().Be(101);
            result.ValidationMessages.Length.Should().Be(0);
        }
    }
}
