using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AFIExercise.Data;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AFIExercise.Tests.Data
{
    public class CustomerRegistrationRepositoryTests : IClassFixture<SqlDbFixture>, IDisposable
    {
        private readonly SqlDbFixture _dbFixture;

        public CustomerRegistrationRepositoryTests(SqlDbFixture dbFixture)
        {
            _dbFixture = dbFixture;
            _dbFixture.CreateScope();
        }

        public void Dispose()
        {
            _dbFixture.DisposeScope();
        }

        [Fact]
        public async Task SavingNewCustomerRegistrationSetsCustomerId()
        {
            var customerRegistration = CreateCompleteCustomerRegistrationInstance();

            customerRegistration.Id.Should().Be(0);

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            await _dbFixture.UnitOfWork.SaveChangesAsync();

            AssertCustomerIdValueAssignedByDb(customerRegistration);
        }

        private static void AssertCustomerIdValueAssignedByDb(CustomerRegistration customerRegistration)
        {
            customerRegistration.Id.Should().BeGreaterThan(0, "database should provide customer ID");
        }

        private static CustomerRegistration CreateCompleteCustomerRegistrationInstance()
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
            var customerRegistration = CreateCompleteCustomerRegistrationInstance();

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            await _dbFixture.UnitOfWork.SaveChangesAsync();

            AssertCustomerIdValueAssignedByDb(customerRegistration);

            var dbCustomerRegistration = await _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Get(customerRegistration.Id);

            dbCustomerRegistration.Should().NotBeNull();
            dbCustomerRegistration.Should().BeEquivalentTo(customerRegistration);
        }

        private void SaveChangesShouldThrowNestedDbUpdateException(string expectedMessage)
        {
            Func<Task> saveAction = async () => await _dbFixture.UnitOfWork.SaveChangesAsync();


            saveAction.Should().Throw<DbUpdateException>().WithInnerException<SqlException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void SavingNewCustomerRegistrationWithMissingFirstNameCausesDbUpdateException()
        {
            var customerRegistration = CreateCompleteCustomerRegistrationInstance();
            customerRegistration.FirstName = null;

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            SaveChangesShouldThrowNestedDbUpdateException("Cannot insert the value NULL into column 'FirstName'*");
        }

        [Fact]
        public void SavingNewCustomerRegistrationWithMissingSurnameCausesDbUpdateException()
        {
            var customerRegistration = CreateCompleteCustomerRegistrationInstance();
            customerRegistration.Surname = null;

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            SaveChangesShouldThrowNestedDbUpdateException("Cannot insert the value NULL into column 'Surname'*");
        }

        [Fact]
        public void SavingNewCustomerRegistrationWithMissingPolicyNumberCausesDbUpdateException()
        {
            var customerRegistration = CreateCompleteCustomerRegistrationInstance();
            customerRegistration.PolicyNumber = null;

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            SaveChangesShouldThrowNestedDbUpdateException("Cannot insert the value NULL into column 'PolicyNumber'*");
        }

        [Fact]
        public void SavingNewCustomerRegistrationWithFirstNameLongerThan50CharactersCausesDbUpdateException()
        {
            var customerRegistration = CreateCompleteCustomerRegistrationInstance();
            customerRegistration.FirstName = new string('A', 51);

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            SaveChangesShouldThrowNestedDbUpdateException("String or binary data would be truncated.*");
        }

        [Fact]
        public void SavingNewCustomerRegistrationWithSurnameLongerThan50CharactersCausesDbUpdateException()
        {
            var customerRegistration = CreateCompleteCustomerRegistrationInstance();
            customerRegistration.Surname = new string('A', 51);

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            SaveChangesShouldThrowNestedDbUpdateException("String or binary data would be truncated.*");
        }

        [Fact]
        public void SavingNewCustomerRegistrationWithPolicyNumberLongerThan9CharactersCausesDbUpdateException()
        {
            var customerRegistration = CreateCompleteCustomerRegistrationInstance();
            customerRegistration.PolicyNumber = new string('A', 10);

            _dbFixture.UnitOfWork.CustomerCustomerRegistrations.Add(customerRegistration);

            SaveChangesShouldThrowNestedDbUpdateException("String or binary data would be truncated.*");
        }
    }
}
