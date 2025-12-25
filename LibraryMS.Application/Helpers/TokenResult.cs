using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Helpers
{
    /// <summary>
    /// Immutable record representing the result of JWT token generation
    /// Contains the generated token string and its expiration date
    /// </summary>
    /// <param name="Token">The JWT token string</param>
    /// <param name="ExpiresAt">The exact date and time when the token expires</param>
    public record TokenResult(string Token, DateTime ExpiresAt);
}

