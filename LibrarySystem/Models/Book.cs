using System;
using System.Collections.Generic;

namespace LibrarySystem.Models;

public partial class Book
{
    public int Id { get; set; } //Identity

    public string? Title { get; set; }

    public int? PublishedYear { get; set; }

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<BookCheckout> BookCheckouts { get; set; } = new List<BookCheckout>();
}
