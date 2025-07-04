﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MoneyFlow.Repositories.Models;

public partial class Currency
{
    public string Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public double ExchangeRate { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}