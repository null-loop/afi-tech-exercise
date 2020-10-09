using System;

namespace AFIExercise.Data
{
    public class CustomerRegistration
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
    }
}