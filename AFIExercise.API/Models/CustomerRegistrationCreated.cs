using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFIExercise.API.Models
{
    public class CustomerRegistrationCreated
    {
        public int CustomerId { get; set; }
    }

    public class CustomerRegistrationRequest
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
    }

    public class ValidationMessage
    {
        public string Property { get; set; }
        public string Message { get; set; }
    }
}
