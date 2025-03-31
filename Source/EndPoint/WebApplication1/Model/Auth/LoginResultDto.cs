namespace Planning.Api.Model.Auth
{
    public class LoginResultDto
    {
        public string? Token { get; set; }
    }
    public class VerificationEmailViewModel
    {
        public string token { get; set; }
    }
}
