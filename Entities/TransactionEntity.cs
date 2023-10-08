using System;
using System.Collections.Generic;
using GeneraStoreApi.Data;
using GeneraStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using GeneraStoreApi.Entities;


namespace GeneraStoreApi.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfTransaction { get; set; }
        [JsonIgnore]
        public virtual CustomerEntity Customer { get; set; }
        [JsonIgnore]
        public virtual ProductEntity Product { get; set; }
    }
}