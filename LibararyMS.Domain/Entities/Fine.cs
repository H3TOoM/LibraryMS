using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Entities
{
    public class Fine
    {
        public int Id { get; set; }
        public int BorrowRecordId { get; set; }
        public double Amount { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime PaidAt { get; set; }
        public BorrowRecord BorrowRecord { get; set; }
    }
}
