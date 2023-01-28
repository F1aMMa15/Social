namespace Social.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid? GetUserId(this HttpContext context)
        {
            var claim = context.User.FindFirst("Id");
            var result = Guid.TryParse(claim?.Value, out var guid);
            if (result)
            {
                return guid;
            }

            return null;
        } 
    }
}
