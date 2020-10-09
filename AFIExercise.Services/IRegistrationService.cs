using System.Text;
using System.Transactions;

namespace AFIExercise.Services
{
    public interface IRegistrationService
    {
        RegistrationResult Register(RegistrationRequest registrationRequest);
    }
}
