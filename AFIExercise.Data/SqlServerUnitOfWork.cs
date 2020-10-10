using System;
using System.Threading.Tasks;

namespace AFIExercise.Data
{
    public class SqlServerUnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly SqlServerDbContext _dbContext;
        private readonly Lazy<ICustomerRegistrationRepository> _customerRegistrationRepository;

        public SqlServerUnitOfWork(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
            _customerRegistrationRepository = new Lazy<ICustomerRegistrationRepository>(() => new SqlServerCustomerRegistrationRepository(dbContext));
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public ICustomerRegistrationRepository CustomerCustomerRegistrations => _customerRegistrationRepository.Value;
        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}