using Social.Application.Enums;

namespace Social.Application.Models
{
    public class Error
    {
        public Error(ErrorCode code, string message)
        {
            ErrorCode = code;
            Messages.Add(message);
        }

        public ErrorCode ErrorCode { get; private set; }
        public List<string> Messages { get; private set; } = new List<string>();
    }
}
