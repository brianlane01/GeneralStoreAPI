using System;
using System.Collections.Generic;
using GeneraStoreApi.Data;
using GeneraStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneraStoreApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace GeneraStoreApi.Models
{
    public class TransactionMultipleEdit : Transaction
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
        public List<int> Quantities { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public DateTime DateOfTransaction { get; set; }
    }
}