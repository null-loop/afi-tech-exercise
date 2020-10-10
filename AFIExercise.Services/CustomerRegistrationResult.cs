using System;
using System.Collections.Generic;
using System.Linq;

namespace AFIExercise.Services
{
    public class CustomerRegistrationResult : Result
    {
        internal CustomerRegistrationResult(params ValidationMessage[] validationMessages) : base(validationMessages)
        {
            if (validationMessages.Length == 0)
            {
                var validationMessagesName = nameof(validationMessages);
                throw new ArgumentException($"Cannot create CustomerRegistrationResult with empty {validationMessagesName}", validationMessagesName);
            }
        }

        internal CustomerRegistrationResult(int customerId) : base(Enumerable.Empty<ValidationMessage>())
        {
            CustomerId = customerId;
        }

        public int? CustomerId { get; }
    }
}