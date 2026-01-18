using LibraryMS.Application.Features.Tokens.Commands.GenerateToken;
using LibraryMS.Application.Helpers;
using LibraryMS.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryMS.Application.Features.Tokens.Commands.GenerateToken;

public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, TokenResult>
{
    private readonly JwtSettings _jwtSettings;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public GenerateTokenCommandHandler(IOptions<JwtSettings> jwtSettings, JwtSecurityTokenHandler jwtSecurityTokenHandler)
    {
        _jwtSettings = jwtSettings.Value;
        _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
    }

    public Task<TokenResult> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        if (request.Member == null)
            throw new ArgumentNullException(nameof(request.Member), "Member cannot be null.");

        // Create signing key from JWT settings
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        // Create JWT claims with member information
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Member.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, request.Member.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, request.Member.Email),
            new Claim(ClaimTypes.Role, request.Member.Role)
        };

        // Set token expiration time
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

        // Configure token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = credentials
        };

        // Generate and serialize the JWT token
        var securityToken = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        var serializedToken = _jwtSecurityTokenHandler.WriteToken(securityToken);

        return Task.FromResult(new TokenResult(serializedToken, expiresAt));
    }
}
