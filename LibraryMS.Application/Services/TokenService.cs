using LibraryMS.Application.Helpers;
using LibraryMS.Application.Interfaces;
using LibraryMS.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryMS.Application.Services
{
    /// <summary>
    /// Service responsible for JWT token generation and management
    /// Creates access tokens for authenticated users
    /// </summary>
    public class TokenService : ITokenService
    {
        #region Fields

        private readonly JwtSettings _jwtSettings;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the TokenService
        /// </summary>
        /// <param name="jwtSettings">JWT configuration settings</param>
        /// <param name="jwtSecurityTokenHandler">JWT security token handler</param>
        public TokenService(IOptions<JwtSettings> jwtSettings, JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtSettings = jwtSettings.Value;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        #endregion

        #region Token Generation

        /// <summary>
        /// Generates a JWT access token for an authenticated member
        /// </summary>
        /// <param name="member">Authenticated member entity</param>
        /// <returns>TokenResult containing the JWT token and expiration date</returns>
        TokenResult ITokenService.GenerateToken(Member member)
        {
            // Create signing key from JWT settings
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Create JWT claims with member information
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, member.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, member.Email),
                new Claim(ClaimTypes.Role, member.Role)
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

            return new TokenResult(serializedToken, expiresAt);
        }

        #endregion
    }
}
