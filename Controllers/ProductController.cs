using System;
using System.Collections.Generic;
using GeneraStoreApi.Data;
using GeneraStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneraStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private GeneralStoreDbContext _db; 
        public ProductController(GeneralStoreDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest request)
        {
            Product newProduct = new()
            {
                Name = request.Name,
                Price = request.Price,
                QuantityInStock = request.Quantity
            };

            _db.Products.Add(newProduct);
            await _db.SaveChangesAsync();
            return Ok(newProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _db.Products
                .Include(e => e.Transactions)
                .ToListAsync();
            return Ok(products);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductEdit model, [FromRoute] int id)
        {
            var oldProduct = await _db.Products.FindAsync(id);
            if(oldProduct == null)
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(!string.IsNullOrEmpty(model.Name))
            {
                oldProduct.Name = model.Name;
            }

            oldProduct.Price = model.Price;
            oldProduct.QuantityInStock = model.QuantityInStock;
            

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct([FromBody] Product request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product? product = await _db.Products.FindAsync(request.Id);
            if (product is null)
            {
                return NotFound();
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.QuantityInStock = request.QuantityInStock;

            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult>DeleteProduct([FromRoute]int id)
        {
            var product = await _db.Products.FindAsync(id);
            if(product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}