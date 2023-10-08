using GeneraStoreApi.Data;
using GeneraStoreApi.Models;
using GeneraStoreApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GeneraStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private GeneralStoreDbContext _db;

        public TransactionController(GeneralStoreDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromForm] TransactionEdit newTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Transaction transaction = new Transaction()
            {
                ProductId = newTransaction.ProductId,
                CustomerId = newTransaction.CustomerId,
                Quantity = newTransaction.Quantity,
                DateOfTransaction = DateTime.Now
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();

            var transactionReport = _db.Transactions
                .Include(e => e.Customer)
                .Include(e => e.Product)
                .FirstOrDefault(e => e.Id == transaction.Id);

            TransactionListItem newTransactionListItem = new TransactionListItem()
            {
                Id = transactionReport.Id,
                ProductName = transactionReport.Product.Name,
                CustomerName = transactionReport.Customer.Name,
                Quantity = transactionReport.Quantity
            };

            return Ok(newTransactionListItem);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transaction = await _db.Transactions
                .Include(e => e.Customer)
                .Include(e => e.Product)
                .ToListAsync();

            var transactionListItems = transaction.Select(transaction => new TransactionListItem
            {
                Id = transaction.Id,
                ProductName = transaction.Product.Name,
                CustomerName = transaction.Customer.Name,
                Quantity = transaction.Quantity,
                Price = transaction.Product.Price
            }).ToList();

            return Ok(transactionListItems);
        }

        [HttpGet]
        [Route("{customerid:int}")]
        public async Task<IActionResult> GetTransactionByCustomerId(int customerId)
        {
            var transactions = await _db.Transactions
                .Where(e => e.CustomerId == customerId)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet]
        [Route("TotalAmountSpent/{customerID}")]
        public async Task<IActionResult> GetTotalAmountSpentByCustomer(int customerId)
        {
            double totalAmountSpent = await _db.Transactions
            .Where(e => e.CustomerId == customerId)
            .Include(e => e.Customer)
            .Include(e => e.Product)
            .SumAsync(e => e.Product.Price);

            return Ok(totalAmountSpent);
        }

        // [HttpPost]
        // public async Task<IActionResult> CreateTransactions([FromBody] List<TransactionEdit> newTransactions)
        // {
        //     var transactionListItems = new List<TransactionListItem>();

        //     foreach (var newTransaction in newTransactions)
        //     {
        //         Transaction transaction = new Transaction()
        //         {
        //             ProductId = newTransaction.ProductId,
        //             CustomerId = newTransaction.CustomerId,
        //             Quantity = newTransaction.Quantity,
        //             DateOfTransaction = DateTime.Now
        //         };

        //         _db.Transactions.Add(transaction);

        //         // Create a transaction list item for each created transaction
        //         var transactionReport = await _db.Transactions
        //             .Include(e => e.Customer)
        //             .Include(e => e.Product)
        //             .FirstOrDefaultAsync(e => e.Id == transaction.Id);

        //         var newTransactionListItem = new TransactionListItem()
        //         {
        //             Id = transactionReport.Id,
        //             ProductName = transactionReport.Product.Name,
        //             CustomerName = transactionReport.Customer.Name,
        //             Quantity = transactionReport.Quantity
        //         };

        //         transactionListItems.Add(newTransactionListItem);
        //     }

        //     await _db.SaveChangesAsync();

        //     return Ok(transactionListItems);
        // }

        // [HttpPost]
        // public async Task<IActionResult> CreateTransaction([FromForm] TransactionMultipleEdit newTransaction)
        // {
        //     var  transaction = new Transaction()
        //     {
        //         CustomerId = newTransaction.CustomerId,
        //         ProductIds = newTransaction.ProductIds,
        //         Quantities = newTransaction.Quantities,
        //         DateOfTransaction = DateTime.Now
        //     };

        //     _db.Transactions.Add(transaction);
        //     await _db.SaveChangesAsync();

        //     var transactionReport = _db.Transactions
        //         .Include(e => e.Customer)
        //         .Include(e => e.Product)
        //         .FirstOrDefault(e => e.Id == transaction.Id);

        //     TransactionListItem newTransactionListItem = new TransactionListItem()
        //     {
        //         Id = transactionReport.Id,
        //         ProductName = transactionReport.Product.Name,
        //         CustomerName = transactionReport.Customer.Name,
        //         Quantity = transactionReport.Quantity
        //     };

        //     return Ok(newTransactionListItem);
        // }
    }
}