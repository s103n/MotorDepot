namespace MotorDepot.BLL.Infrastructure
{
    public class ValidationStatus
    {
        public string Message { get; set; }
        public string Property { get; set; }
        public bool Success { get; set; }

        public ValidationStatus(string message, string property, bool success)
        {
            Message = message;
            Property = property;
            Success = success;
        }

        public ValidationStatus(string message, bool success)
        {
            Success = success;
            Message = message;
        }
    }
}
