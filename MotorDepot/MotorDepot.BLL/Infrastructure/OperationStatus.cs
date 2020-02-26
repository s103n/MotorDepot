namespace MotorDepot.BLL.Infrastructure
{
    public sealed class OperationStatus
    {
        public string Property { get; }
        public string Message { get; }
        public bool Success { get; }

        public OperationStatus(string property, string message, bool success)
        {
            Property = property;
            Message = message;
            Success = success;
        }
    }
}
