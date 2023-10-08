using System;
using System.Collections.Generic;
using GeneraStoreApi.Data;
using GeneraStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GeneraStoreApi.Models;

public partial class Transaction 
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public int Quantity { get; set; }

    public DateTime DateOfTransaction { get; set; }

    [JsonIgnore]
    public virtual Customer Customer { get; set; } = null!;

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
}
