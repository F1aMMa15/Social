using Social.Application.Enums;

namespace Social.Application.Models
{
    public class OperationResult<T>
    {
        public T? Payload { get; set; }
        public bool IsError { get; private set; }
        public Error? Error { get; private set; }

        public void SetError(ErrorCode code, string message)
        {
            IsError = true;
            Error = new Error(code, message);
        }

        public void AddErrorMessage(string message)
        {
            Error?.Messages.Add(message);
        }
    }
}
