using System;
using System.Collections.Generic;
using GeneraStoreApi.Data;
using GeneraStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneraStoreApi.Entities;
using Newtonsoft.Json;

namespace GeneraStoreApi.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public virtual List<TransactionEntity> Transactions { get; set; }
    }
}