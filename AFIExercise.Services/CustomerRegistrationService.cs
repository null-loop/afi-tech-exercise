using System;
using System.Linq;
using System.Threading.Tasks;
using AFIExercise.Data;

namespace AFIExercise.Services
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomerRegistrationRequestValidator _validator;

        public CustomerRegistrationService(IUnitOfWork unitOfWork, CustomerRegistrationRequestValidator validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<CustomerRegistrationResult> Register(CustomerRegistrationRequest registrationRequest)
        {
            var validationResult = await _validator.ValidateAsync(registrationRequest);

            if (!validationResult.IsValid)
            {
                return new CustomerRegistrationResult(validationResult.Errors.Select(e => new ValidationMessage(e.PropertyName, e.ErrorMessage)).ToArray());
            }

            var customerRegistration = new CustomerRegistration
            {
                FirstName = registrationRequest.FirstName,
                Surname = registrationRequest.Surname,
                PolicyNumber = registrationRequest.PolicyNumber,
                DateOfBirth = registrationRequest.DateOfBirth,
                EmailAddress = registrationRequest.EmailAddress
            };

            _unitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            await _unitOfWork.SaveChangesAsync();

            return new CustomerRegistrationResult(customerRegistration.Id);
        }
    }
}