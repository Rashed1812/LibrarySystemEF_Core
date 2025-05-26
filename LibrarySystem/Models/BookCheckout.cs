using System;
using System.Collections.Generic;

namespace LibrarySystem.Models;

public partial class BookCheckout
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public int? MemberId { get; set; }

    public DateOnly? CheckoutDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Member? Member { get; set; }
}
