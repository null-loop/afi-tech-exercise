using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AFIExercise.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Xunit;

namespace AFIExercise.Tests.Services
{
    public class CustomerRegistrationRequestValidatorTests
    {
        [Fact]
        public void PersonBornOn29thFebHas18thBirthdayOn1stMar()
        {
            var today = new DateTime(2018, 3, 1);
            var dateOfBirth = new DateTime(2000,2,29);

            var age = CustomerRegistrationRequestValidator.CalculateCurrentAgeInYears(today, dateOfBirth);

            age.Should().Be(18);
        }

        [Fact]
        public void PersonBornOn29thFebIs17On28thFeb()
        {
            var today = new DateTime(2018, 2, 28);
            var dateOfBirth = new DateTime(2000, 2, 29);

            var age = CustomerRegistrationRequestValidator.CalculateCurrentAgeInYears(today, dateOfBirth);

            age.Should().Be(17);
        }

        [Fact]
        public void PersonBornOn1stJanHas18thBirthdayOn1stJan()
        {
            var today = new DateTime(2018, 1, 1);
            var dateOfBirth = new DateTime(2000, 1, 1);

            var age = CustomerRegistrationRequestValidator.CalculateCurrentAgeInYears(today, dateOfBirth);

            age.Should().Be(18);
        }

        [Fact]
        public void PersonBornOn1stJanIs17On31stDec()
        {
            var today = new DateTime(2017,12,31);
            var dateOfBirth = new DateTime(2000, 1, 1);

            var age = CustomerRegistrationRequestValidator.CalculateCurrentAgeInYears(today, dateOfBirth);

            age.Should().Be(17);
        }

        private static CustomerRegistrationRequest CreateCompleteCustomerRegistrationRequest()
        {
            return new CustomerRegistrationRequest
            {
                FirstName = "Bob",
                Surname = "Smith",
                DateOfBirth = new DateTime(2000, 1, 1),
                EmailAddress = "robert@smith.co.uk",
                PolicyNumber = "AB-123456"
            };
        }

        private static readonly CustomerRegistrationRequestValidator Validator = new CustomerRegistrationRequestValidator();

        [Fact]
        public void FullyPopulatedHasNoErrors()
        {
            var request = CreateCompleteCustomerRegistrationRequest();

            CheckValidationResult(request);
        }

        [Fact]
        public void FullyPopulatedWithoutEmailAddressHasNoErrors()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.EmailAddress = null;

            CheckValidationResult(request);
        }

        [Fact]
        public void FullyPopulatedWithoutDateOfBirthHasNoErrors()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.DateOfBirth = null;

            CheckValidationResult(request);
        }

        [Fact]
        public void FirstNameNull()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.FirstName = null;

            CheckValidationResult(request, new Tuple<string, string>("FirstName", "'First Name' must not be empty."));
        }

        [Fact]
        public void FirstNameEmpty()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.FirstName = string.Empty;

            CheckValidationResult(request, new Tuple<string, string>("FirstName", "'First Name' must be between 3 and 50 characters. You entered 0 characters."));
        }

        [Fact]
        public void FirstNameTooShort()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.FirstName = "AB";

            CheckValidationResult(request, new Tuple<string, string>("FirstName", "'First Name' must be between 3 and 50 characters. You entered 2 characters."));
        }

        [Fact]
        public void FirstNameTooLong()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.FirstName = new string('A',51);

            CheckValidationResult(request, new Tuple<string, string>("FirstName", "'First Name' must be between 3 and 50 characters. You entered 51 characters."));
        }

        [Fact]
        public void SurnameNull()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.Surname = null;

            CheckValidationResult(request, new Tuple<string, string>("Surname", "'Surname' must not be empty."));
        }

        [Fact]
        public void SurnameEmpty()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.Surname = string.Empty;

            CheckValidationResult(request, new Tuple<string, string>("Surname", "'Surname' must be between 3 and 50 characters. You entered 0 characters."));
        }

        [Fact]
        public void SurnameTooShort()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.Surname = "AB";

            CheckValidationResult(request, new Tuple<string, string>("Surname", "'Surname' must be between 3 and 50 characters. You entered 2 characters."));
        }

        [Fact]
        public void SurnameTooLong()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.Surname = new string('A', 51);

            CheckValidationResult(request, new Tuple<string, string>("Surname", "'Surname' must be between 3 and 50 characters. You entered 51 characters."));
        }

        [Fact]
        public void PolicyNumberNull()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.PolicyNumber = null;

            CheckValidationResult(request, new Tuple<string, string>("PolicyNumber", "'Policy Number' must not be empty."));
        }

        [Fact]
        public void PolicyNumberEmpty()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.PolicyNumber = string.Empty;

            CheckValidationResult(request, new Tuple<string, string>("PolicyNumber", "'Policy Number' must be 9 characters in length. You entered 0 characters."),
                new Tuple<string, string>("PolicyNumber", "'Policy Number' is not in the correct format."));
        }

        [Fact]
        public void PolicyNumberTooShort()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.PolicyNumber = new string('A', 8);

            CheckValidationResult(request, new Tuple<string, string>("PolicyNumber", "'Policy Number' must be 9 characters in length. You entered 8 characters."),
                new Tuple<string, string>("PolicyNumber", "'Policy Number' is not in the correct format."));
        }

        [Fact]
        public void PolicyNumberTooLong()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.PolicyNumber = new string('A', 10);

            CheckValidationResult(request, new Tuple<string, string>("PolicyNumber", "'Policy Number' must be 9 characters in length. You entered 10 characters."),
                new Tuple<string, string>("PolicyNumber", "'Policy Number' is not in the correct format."));
        }

        [Fact]
        public void PolicyFormatWrongFormatAllCharacters()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.PolicyNumber = "AB-ABCDEF";

            CheckValidationResult(request, new Tuple<string, string>("PolicyNumber", "'Policy Number' is not in the correct format."));
        }

        [Fact]
        public void PolicyFormatWrongFormatAllNumbers()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.PolicyNumber = "12-123456";

            CheckValidationResult(request, new Tuple<string, string>("PolicyNumber", "'Policy Number' is not in the correct format."));
        }

        [Fact]
        public void PolicyFormatWrongFormatCharactersNumbersSwapped()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.PolicyNumber = "12-ABCDEF";

            CheckValidationResult(request, new Tuple<string, string>("PolicyNumber", "'Policy Number' is not in the correct format."));
        }

        [Fact]
        public void PolicyFormatWrongFormatHyphenReplacedWithCharacter()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.PolicyNumber = "ABC123456";

            CheckValidationResult(request, new Tuple<string, string>("PolicyNumber", "'Policy Number' is not in the correct format."));
        }

        [Fact]
        public void MissingEmailAddressAndDateOfBirth()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.DateOfBirth = null;
            request.EmailAddress = null;

            CheckValidationResult(request, new Tuple<string, string>(string.Empty, "'Date Of Birth' or 'Email Address' must not be empty."));
        }

        [Fact]
        public void RegistrantNot18YearsOld()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.DateOfBirth = DateTime.Today.AddYears(-18).AddDays(7);

            CheckValidationResult(request, new Tuple<string, string>("DateOfBirth", "Registrant must be aged 18 years or older on date of registration."));
        }

        [Fact]
        public void EmailAddressHasInvalidTopLevelDomain()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.EmailAddress = "robert@smith.org";

            CheckValidationResult(request, new Tuple<string, string>("EmailAddress", "'Email Address' is not in the correct format."));
        }

        [Fact]
        public void EmailAddressUsernameTooShort()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.EmailAddress = "rob@smith.co.uk";

            CheckValidationResult(request, new Tuple<string, string>("EmailAddress", "'Email Address' is not in the correct format."));
        }

        [Fact]
        public void EmailAddressDomainTooShort()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.EmailAddress = "rob@s.co.uk";

            CheckValidationResult(request, new Tuple<string, string>("EmailAddress", "'Email Address' is not in the correct format."));
        }

        [Fact]
        public void EmailAddressUsernameContainsInvalidCharacter()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.EmailAddress = "rob+afi@smith.co.uk";

            CheckValidationResult(request, new Tuple<string, string>("EmailAddress", "'Email Address' is not in the correct format."));
        }

        [Fact]
        public void EmailAddressDomainContainsInvalidCharacter()
        {
            var request = CreateCompleteCustomerRegistrationRequest();
            request.EmailAddress = "rob@smith-household.co.uk";

            CheckValidationResult(request, new Tuple<string, string>("EmailAddress", "'Email Address' is not in the correct format."));
        }

        private static void CheckValidationResult(CustomerRegistrationRequest request, params Tuple<string, string>[] errorMessages)
        {
            var result = Validator.Validate(request);
            var isValid = errorMessages.Length == 0;
            result.IsValid.Should().Be(isValid);
            result.Errors.Should().HaveCount(errorMessages.Length);

            for (var i = 0; i != errorMessages.Length; i++)
            {
                var error = result.Errors[i];

                error.PropertyName.Should().Be(errorMessages[i].Item1);
                error.ErrorMessage.Should().Be(errorMessages[i].Item2);
            }
        }
    }
}
