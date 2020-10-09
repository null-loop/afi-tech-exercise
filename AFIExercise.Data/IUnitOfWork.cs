using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFIExercise.Data
{
    public interface IUnitOfWork
    {
        ICustomerRegistrationRepository CustomerCustomerRegistrations { get; }

        Task SaveAsync();
    }
}
