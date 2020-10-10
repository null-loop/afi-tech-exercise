using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;

namespace AFIExercise.Services
{
    public class CustomerRegistrationRequestValidator : AbstractValidator<CustomerRegistrationRequest>
    {
        private static readonly Regex EmailAddressRegex = new Regex("[A-Za-z0-9]{4,}\\@[A-Za-z0-9]{2,}(.co.uk|.com)");

        public CustomerRegistrationRequestValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty().Length(3, 50);
            RuleFor(r => r.Surname).NotEmpty().Length(3, 50);
            RuleFor(r => r.PolicyNumber).NotEmpty().Length(9).Matches("[A-Z]{2}\\-[0-9]{6}");

            RuleFor(r => r).Custom((request, context) =>
            {
                var dateOfBirth = request.DateOfBirth;
                var emailAddress = request.EmailAddress;
                if (!dateOfBirth.HasValue && string.IsNullOrEmpty(emailAddress))
                {
                    context.AddFailure($"{nameof(CustomerRegistrationRequest.DateOfBirth)} or {nameof(CustomerRegistrationRequest.EmailAddress)} must be supplied");
                }
                else
                {
                    if (dateOfBirth.HasValue)
                    {
                        var currentAge = CalculateCurrentAgeInYears(DateTime.Today, dateOfBirth.Value);
                        if (currentAge < 18)
                        {
                            context.AddFailure(nameof(CustomerRegistrationRequest.DateOfBirth), "Registrant must be aged 18 years or older on date of registration");
                        }
                    }

                    if (!string.IsNullOrEmpty(emailAddress))
                    {
                        var match = EmailAddressRegex.Match(emailAddress);
                        if (match.Length != emailAddress.Length)
                        {
                            context.AddFailure(nameof(CustomerRegistrationRequest.EmailAddress), "Invalid email address format");
                        }
                    }
                }
            });
        }

        public static int CalculateCurrentAgeInYears(DateTime today, DateTime dateOfBirth)
        {
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
