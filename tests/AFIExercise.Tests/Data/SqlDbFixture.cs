using AFIExercise.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AFIExercise.Tests.Data
{
    public class SqlDbFixture
    {
        private readonly ServiceProvider _serviceProvider;
        private IServiceScope _scope;
        public IUnitOfWork UnitOfWork { get; private set; }

        public SqlDbFixture()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .AddEnvironmentVariables()
                .Build();

            serviceCollection.AddSqlServerDataAccess(configuration);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var dbContext = _serviceProvider.GetService<SqlServerDbContext>();

            dbContext.Database.EnsureCreated();
            dbContext.Dispose();
        }


        public void CreateScope()
        {
            _scope = _serviceProvider.CreateScope();

            UnitOfWork = _scope.ServiceProvider.GetService<IUnitOfWork>();
        }

        public void DisposeScope()
        {
            _scope.Dispose();
        }
    }
}