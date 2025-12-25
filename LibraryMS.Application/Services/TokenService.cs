using LibraryMS.Application.Helpers;
using LibraryMS.Application.Interfaces;
using LibraryMS.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryMS.Application.Services
{
    public class TokenService : ITokenService
    {

        private readonly JwtSettings _jwtSettings;  
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        public TokenService(JwtSettings jwtSettings, JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtSettings = jwtSettings;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        TokenResult ITokenService.GenerateToken(Member member)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, member.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, member.Email),
                new Claim(ClaimTypes.Role, member.Role)
            };

            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = credentials
            };

            var securityToken = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            var serializedToken = _jwtSecurityTokenHandler.WriteToken(securityToken);

            return new TokenResult(serializedToken, expiresAt);
        }
    }
}
