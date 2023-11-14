namespace CoreCrudApi.Models
{
    public class AuthResponseDto
    {
        public bool IsUserAuthenticated { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresIn { get; set; }
    }
}
