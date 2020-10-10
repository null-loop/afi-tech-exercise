using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AFIExercise.Data
{
    public class SqlServerCustomerRegistrationRepository : ICustomerRegistrationRepository
    {
        private readonly SqlServerDbContext _dbContext;

        public SqlServerCustomerRegistrationRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(CustomerRegistration customerRegistration)
        {
            _dbContext.CustomerRegistrations.Add(customerRegistration);
        }

        public Task<CustomerRegistration> Get(int customerRegistrationId)
        {
            return _dbContext.CustomerRegistrations.FirstOrDefaultAsync(cr => cr.Id == customerRegistrationId);
        }
    }
}