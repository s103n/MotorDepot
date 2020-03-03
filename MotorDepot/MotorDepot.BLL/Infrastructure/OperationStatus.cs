using System.Net;

namespace MotorDepot.BLL.Infrastructure
{
    //IOperationStatus
    public sealed class OperationStatus<T> where T : class
    {
        public T Value { get; }
        public string Message { get; }
        public HttpStatusCode Code { get; }

        public OperationStatus(string message, HttpStatusCode code, T value)
        {
            Message = message;
            Code = code;
            Value = value;
        }
    }

    public sealed class OperationStatus
    {
        public string Message { get; }
        public HttpStatusCode Code { get; set; }


        public OperationStatus(string message, HttpStatusCode code)
        {
            Message = message;
            Code = code;
        }
    }
}
