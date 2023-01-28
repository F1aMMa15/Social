namespace Social.Api.Models
{
    public class ErrorResponse
    {
        public string PropertyName { get; set; } = null!;
        public List<string> ErrorMessages { get; set; } = new List<string>();   
    }
}
