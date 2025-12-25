using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Helpers;
using LibraryMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Interfaces
{
    /// <summary>
    /// Interface for JWT token generation services
    /// Defines methods for creating access tokens for authenticated users
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT access token for an authenticated member
        /// </summary>
        /// <param name="member">Authenticated member entity</param>
        /// <returns>TokenResult containing the JWT token and expiration date</returns>
        TokenResult GenerateToken(Member member);
    }
}
