using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Helpers
{
    /// <summary>
    /// Configuration class for JWT (JSON Web Token) settings
    /// Contains all necessary parameters for JWT token generation and validation
    /// </summary>
    public class JwtSettings
    {
        #region JWT Configuration Properties

        /// <summary>
        /// The issuer of the JWT token (typically the application name or URL)
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// The intended audience for the JWT token (typically the client application)
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// The secret key used for signing JWT tokens
        /// Must be kept secure and not shared publicly
        /// </summary>
        public string SigningKey { get; set; } = string.Empty;

        /// <summary>
        /// Token expiration time in minutes (default: 60 minutes)
        /// </summary>
        public int ExpiryMinutes { get; set; } = 60;

        #endregion
    }
}
