﻿using System;
using System.Collections.Generic;

namespace MusicStore.DAL.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int Userid { get; set; }

    public string Address { get; set; } = null!;

    public decimal Total { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}