using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AFIExercise.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Xunit;

namespace AFIExercise.Tests.Services
{
    public class CustomerRegistrationRequestValidatorTests
    {
        [Fact]
        public void PersonBornOn29thFebHas18thBirthdayOn1stMar()
        {
            var today = new DateTime(2018, 3, 1);
            var dateOfBirth = new DateTime(2000,2,29);

            var age = CustomerRegistrationRequestValidator.CalculateCurrentAgeInYears(today, dateOfBirth);

            age.Should().Be(18);
        }

        [Fact]
        public void PersonBornOn29thFebIs17On28thFeb()
        {
            var today = new DateTime(2018, 2, 28);
            var dateOfBirth = new DateTime(2000, 2, 29);

            var age = CustomerRegistrationRequestValidator.CalculateCurrentAgeInYears(today, dateOfBirth);

            age.Should().Be(17);
        }

        [Fact]
        public void PersonBornOn1stJanHas18thBirthdayOn1stJan()
        {
            var today = new DateTime(2018, 1, 1);
            var dateOfBirth = new DateTime(2000, 1, 1);

            var age = CustomerRegistrationRequestValidator.CalculateCurrentAgeInYears(today, dateOfBirth);

            age.Should().Be(18);
        }

        [Fact]
        public void PersonBornOn1stJanIs17On31stDec()
        {
            var today = new DateTime(2017,12,31);
            var dateOfBirth = new DateTime(2000, 1, 1);

            var age = CustomerRegistrationRequestValidator.CalculateCurrentAgeInYears(today, dateOfBirth);

            age.Should().Be(17);
        }

        //TODO:Write tests around rest of CustomerRegistrationRequestValidator
    }
}
