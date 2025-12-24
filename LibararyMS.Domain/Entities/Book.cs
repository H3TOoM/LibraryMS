using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CopiesAvailable { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
