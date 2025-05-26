using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.DTOs
{
    public class BookDto
    {
        public string Title { get; set; } = default!;
        public int? PublishedYear { get; set; }
        public int? AuthorId { get; set; }
    }
}
