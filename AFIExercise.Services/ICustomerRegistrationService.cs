using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AFIExercise.Services
{
    public interface ICustomerRegistrationService
    {
        Task<CustomerRegistrationResult> Register(CustomerRegistrationRequest registrationRequest);
    }
}
