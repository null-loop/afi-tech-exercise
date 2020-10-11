namespace AFIExercise.API.Models
{
    /// <summary>
    /// Type returned from the API when a request fails validation. More than one may be returned.
    /// </summary>
    public class ValidationMessage
    {
        /// <summary>
        /// Property of the original request that failed validation.
        /// </summary>
        public string Property { get; set; }
        /// <summary>
        /// User friendly message explaining validation failure.
        /// </summary>
        public string Message { get; set; }
    }
}