using System;
using AFIExercise.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AFIExercise.Tests.Data
{
    public class SqlDbFixture : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public SqlDbFixture()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder().AddJsonFile("testsettings.json").Build();

            serviceCollection.AddSqlServerDataAccess(configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var dbContext = serviceProvider.GetService<SqlServerDbContext>();

            dbContext.Database.EnsureCreated();

            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}