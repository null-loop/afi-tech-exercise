﻿using System.Collections.Generic;
using System.Linq;

namespace AFIExercise.Services
{
    public abstract class Result
    {
        protected Result(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages.ToArray();
            IsSuccessful = !ValidationMessages.Any();
        }

        public bool IsSuccessful { get; }

        public ValidationMessage[] ValidationMessages { get; }
    }
}