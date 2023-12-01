using System;
using System.Collections.Generic;

namespace backend.Models;

public class Cashback
{
    public int IdCashback { get; set; }

    public int IdTransaction { get; set; }

    public decimal TaxCashback { get; set; }

    public virtual Transaction IdTransactionNavigation { get; set; } = null!;
}
