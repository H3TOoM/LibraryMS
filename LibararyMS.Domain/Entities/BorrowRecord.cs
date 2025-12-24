using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Entities
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public double FineAmount { get; set; }
        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}
