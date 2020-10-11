namespace AFIExercise.API.Models
{
    /// <summary>
    /// Type returned when customer registration is successful.
    /// </summary>
    public class CustomerRegistrationCreated
    {
        /// <summary>
        /// Unique customer ID assigned to the registration.
        /// </summary>
        public int CustomerId { get; set; }
    }
}
