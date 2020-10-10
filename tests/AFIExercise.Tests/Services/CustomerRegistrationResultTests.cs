using System;
using System.Collections.Generic;
using System.Text;
using AFIExercise.Services;
using FluentAssertions;
using Xunit;

namespace AFIExercise.Tests.Services
{
    public class CustomerRegistrationResultTests
    {
        [Fact]
        public void ConstructedWithCustomerIdSetupCorrectly()
        {
            var result = new CustomerRegistrationResult(100);

            result.IsSuccessful.Should().BeTrue();
            result.CustomerId.HasValue.Should().BeTrue();
            result.CustomerId.Should().Be(100);
            result.ValidationMessages.Should().NotBeNull();
            result.ValidationMessages.Should().BeEmpty();
        }

        [Fact]
        public void ConstructedWithValidationMessagesSetupCorrectly()
        {
            var result = new CustomerRegistrationResult(
                new ValidationMessage("FirstName", "Must have a value"),
                new ValidationMessage("Surname", "Must have a value"));

            result.IsSuccessful.Should().BeFalse();
            result.CustomerId.HasValue.Should().BeFalse();
            result.ValidationMessages.Should().NotBeNullOrEmpty();
            result.ValidationMessages.Length.Should().Be(2);

            var messageOne = result.ValidationMessages[0];
            var messageTwo = result.ValidationMessages[1];

            messageOne.Property.Should().Be("FirstName");
            messageOne.Message.Should().Be("Must have a value");

            messageTwo.Property.Should().Be("Surname");
            messageTwo.Message.Should().Be("Must have a value");
        }

        [Fact]
        public void ConstructedWithNoValidationMessagesThrowsArgumentException()
        {
            Action constructionAction = ()=> new CustomerRegistrationResult();
            constructionAction.Should().Throw<ArgumentException>().WithMessage("Cannot create CustomerRegistrationResult with empty validationMessages (Parameter 'validationMessages')");
        }
    }
}
