namespace AFIExercise.Services
{
    public class ValidationMessage
    {
        internal ValidationMessage(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public string Property { get; }
        public string Message { get; }
    }
}