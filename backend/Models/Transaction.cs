using System;
using System.Collections.Generic;

namespace backend.Models;

public class Transaction
{
    public int IdTransaction { get; set; }

    public int IdAccount { get; set; }

    public DateTime TransactionData { get; set; }

    public decimal Value { get; set; }

    public decimal? Cashback { get; set; }

    public virtual ICollection<Cashback> Cashbacks { get; } = new List<Cashback>();

    public virtual Account IdAccountNavigation { get; set; } = null!;
}
