using System;
using System.ComponentModel.DataAnnotations;

namespace AFIExercise.Data
{
    public class CustomerRegistration
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(9)]
        public string PolicyNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string EmailAddress { get; set; }
    }
}