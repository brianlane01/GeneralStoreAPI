using System;
using System.Collections.Generic;
using GeneraStoreApi.Data;
using GeneraStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneraStoreApi.Models
{
    public class CustomerEdit
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}