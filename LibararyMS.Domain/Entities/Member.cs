using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; } = "Member"; // Admin , Member , Guest
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int MaxBorrowLimit { get; set; }

    }
}
