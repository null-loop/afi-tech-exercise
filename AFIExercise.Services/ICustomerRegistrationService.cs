using System.Text;
using System.Transactions;

namespace AFIExercise.Services
{
    public interface ICustomerRegistrationService
    {
        CustomerRegistrationResult Register(CustomerRegistrationRequest registrationRequest);
    }
}
