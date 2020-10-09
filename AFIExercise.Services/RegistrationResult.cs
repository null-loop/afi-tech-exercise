using System.Collections.Generic;
using System.Linq;

namespace AFIExercise.Services
{
    public class RegistrationResult : Result
    {
        internal RegistrationResult(params ValidationMessage[] validationMessages) : base(validationMessages)
        {

        }

        internal RegistrationResult(int customerId) : base(Enumerable.Empty<ValidationMessage>())
        {
            CustomerId = customerId;
        }

        public int? CustomerId { get; }
    }
}