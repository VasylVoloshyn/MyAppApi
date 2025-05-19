using MyApp.Application.DTO.Authentication;

namespace MyApp.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(TokenRequestDto userData);
}
