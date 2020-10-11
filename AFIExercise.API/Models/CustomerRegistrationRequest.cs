﻿using System;

namespace AFIExercise.API.Models
{
    public class CustomerRegistrationRequest
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
    }
}