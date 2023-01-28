namespace Social.Application.Enums
{
    public enum ErrorCode
    {
        Unknown = 0,
        Forbidden = 403,
        NotFound = 404,
        ServerError = 500,

        // Custom codes
        AlreadyRegistred = 220,
        DoesntExist = 225,
        IncorrectPassword = 226,
    }
}
