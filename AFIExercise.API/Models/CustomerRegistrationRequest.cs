using System;

namespace AFIExercise.API.Models
{
    /// <summary>
    /// Type submitted to request the creation of customer registration.
    /// </summary>
    public class CustomerRegistrationRequest
    {
        /// <summary>
        /// Registrants first name. Must be between 3 and 50 characters in length.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Registrants surname. Must be between 3 and 50 characters in length.
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Registrants policy number. Must be in the form AA-NNNNNN (A - Uppercase alpha character, N - number).
        /// </summary>
        public string PolicyNumber { get; set; }
        /// <summary>
        /// Registrants date of birth. Registrant must be at least 18 years of age on date of registration.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Registrants email address. Must in form username@domain.tld where username is at least 4 alphanumeric characters, domain is at least 2 alphanumeric characters and tld one of co.uk or com.
        /// </summary>
        public string EmailAddress { get; set; }
    }
}