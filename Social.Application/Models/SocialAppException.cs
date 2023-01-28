using Social.Application.Enums;

namespace Social.Application.Models
{
    public class SocialAppException : Exception
    {
        public SocialAppException(string message, ErrorCode errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public ErrorCode ErrorCode { get; }

    }
}
