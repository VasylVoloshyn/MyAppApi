namespace MyApp.Application.DTO.Authentication;

public class TokenRequestDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = new List<string>();
}
