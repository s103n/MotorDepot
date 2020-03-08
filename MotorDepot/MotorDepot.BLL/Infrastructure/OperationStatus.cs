using System.Net;

namespace MotorDepot.BLL.Infrastructure
{
    public interface IOperationStatus
    {
        string Message { get; }
        HttpStatusCode Code { get; }
        bool Success { get; }
    }
    //IOperationStatus
    public sealed class OperationStatus<T> : IOperationStatus
    {
        public T Value { get; }
        public string Message { get; }
        public HttpStatusCode Code { get; }
        public bool Success { get; }

        public OperationStatus(string message, HttpStatusCode code, T value, bool success)
        {
            Message = message;
            Code = code;
            Value = value;
        }

        public OperationStatus(string message, T value, bool success)
        {
            Success = success;
            Message = message;
            Value = value;
            Code = HttpStatusCode.OK;
        }

        public OperationStatus(string message, HttpStatusCode code, bool success)
        {
            Message = message;
            Code = code;
            Success = success;
        }

        public OperationStatus(string errorMessage, bool success)
        {
            Success = success;
            Message = errorMessage;
            Code = HttpStatusCode.OK;
        }
    }

    public sealed class OperationStatus : IOperationStatus
    {
        public string Message { get; }
        public HttpStatusCode Code { get; }
        public bool Success { get; }

        public OperationStatus(string message, HttpStatusCode code, bool success)
        {
            Message = message;
            Code = code;
            Success = success;
        }

        public OperationStatus(string errorMessage, bool success)
        {
            Success = success;
            Message = errorMessage;
            Code = HttpStatusCode.OK;
        }
    }
}
