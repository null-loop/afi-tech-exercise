using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFIExercise.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRegistrationRepository CustomerCustomerRegistrations { get; }

        Task SaveChangesAsync();
    }
}
