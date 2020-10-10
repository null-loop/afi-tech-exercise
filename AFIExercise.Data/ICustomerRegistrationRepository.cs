using System.Threading.Tasks;

namespace AFIExercise.Data
{
    public interface ICustomerRegistrationRepository
    {
        void Add(CustomerRegistration customerRegistration);
        Task<CustomerRegistration> Get(int customerRegistrationId);
    }
}