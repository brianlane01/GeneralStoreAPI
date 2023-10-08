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
    public class TransactionListItem
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "# Sold")]
        public int Quantity { get; set; }
        
        [Display(Name = "Price")]
        public double Price { get; set; }
        
    }
}