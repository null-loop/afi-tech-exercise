using System.Threading.Tasks;

namespace AFIExercise.Services
{
    public interface ICustomerRegistrationService
    {
        Task<CustomerRegistrationResult> Register(CustomerRegistrationRequest registrationRequest);
    }
}
