using System;
using System.Threading.Tasks;

namespace AFIExercise.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRegistrationRepository CustomerCustomerRegistrations { get; }

        Task SaveChangesAsync();
    }
}
