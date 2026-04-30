using System.Text.Json.Serialization;

namespace LibraryProject.MAUI.Models;

public class AuthResponse
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; set; }

    public string GetToken()
    {
        if (!string.IsNullOrWhiteSpace(AccessToken)) return AccessToken;
        if (!string.IsNullOrWhiteSpace(Token)) return Token;
        return string.Empty;
    }
}
