using System.Collections.Generic;
using System.Linq;

namespace AFIExercise.Services
{
    public class CustomerRegistrationResult : Result
    {
        internal CustomerRegistrationResult(params ValidationMessage[] validationMessages) : base(validationMessages)
        {

        }

        internal CustomerRegistrationResult(int customerId) : base(Enumerable.Empty<ValidationMessage>())
        {
            CustomerId = customerId;
        }

        public int? CustomerId { get; }
    }
}