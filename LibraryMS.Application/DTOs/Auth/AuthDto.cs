using LibraryMS.Application.DTOs.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryMS.Application.DTOs.Auth
{
    /// <summary>
    /// Data Transfer Object for authentication responses
    /// Contains JWT token information and authenticated member details
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        /// JWT access token string
        /// </summary>
        [Required]
        [Display(Name = "Access Token")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Token expiration date and time (UTC)
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Token Expiration")]
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Details of the authenticated member
        /// </summary>
        [Required]
        [Display(Name = "Member Information")]
        public MemberReadDto Member { get; set; } = default!;
    }
}
