using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AFIExercise.Data;
using FluentAssertions;
using Xunit;

namespace AFIExercise.Tests.Data
{
    public class UnitOfWorkTests : IClassFixture<SqlDbFixture>
    {
        private readonly SqlDbFixture _dbFixture;

        public UnitOfWorkTests(SqlDbFixture dbFixture)
        {
            _dbFixture = dbFixture;
        }

        [Fact]
        public async Task SavingNewCustomerRegistrationSetsCustomerId()
        {
            var customerRegistration = CreateCustomerRegistrationInstance();

            customerRegistration.Id.Should().Be(0);

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            await _dbFixture.UnitOfWork.SaveChangesAsync();

            AssertCustomerIdValueAssignedByDb(customerRegistration);
        }

        private static void AssertCustomerIdValueAssignedByDb(CustomerRegistration customerRegistration)
        {
            customerRegistration.Id.Should().BeGreaterThan(0, "database should provide customer ID");
        }

        private static CustomerRegistration CreateCustomerRegistrationInstance()
        {
            var customerRegistration = new CustomerRegistration
            {
                FirstName = "Bob",
                Surname = "Smith",
                DateOfBirth = new DateTime(2020, 1, 1),
                EmailAddress = "bob@smith.co.uk",
                PolicyNumber = "AB-123456"
            };
            return customerRegistration;
        }

        [Fact]
        public async Task SavedNewCustomerRegistrationCanBeRetrievedByCustomerId()
        {
            var customerRegistration = CreateCustomerRegistrationInstance();

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            await _dbFixture.UnitOfWork.SaveChangesAsync();

            AssertCustomerIdValueAssignedByDb(customerRegistration);

            var dbCustomerRegistration = await _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Get(customerRegistration.Id);

            dbCustomerRegistration.Should().NotBeNull();
            dbCustomerRegistration.Should().BeEquivalentTo(customerRegistration);
        }
    }
}
