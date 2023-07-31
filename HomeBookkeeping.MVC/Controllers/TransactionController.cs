using HomeBookkeeping.Api.Filters;
using HomeBookkeeping.Application.UseCases.Transactions.Commands.CreateTransaction;
using HomeBookkeeping.Application.UseCases.Transactions.Commands.DeleteTransaction;
using HomeBookkeeping.Application.UseCases.Transactions.Commands.UpdateTransaction;
using HomeBookkeeping.Application.UseCases.Transactions.Queries.GetAllTransactions;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolMonitoringSystem.Api.Controllers;
using X.PagedList;

namespace HomeBookkeeping.Controllers
{
    public class TransactionController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> CreateTransaction()
        {
            //ViewData["subjects"] = subjects;
            //ViewData["students"] = students;
            return View();
        }
        [HttpPost]
        [ActionModelValidationAttribute]
        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand TransactionCommand)
        {
            var result = await Mediator.Send(TransactionCommand);
            return RedirectToAction(nameof(GetAllTransactions));
        }
        [HttpGet]
        [EnableRateLimiting("Token")]
        public async Task<IActionResult> GetAllTransactions(int page = 1)
        {
            //ViewData["students"] = await Mediator.Send(new GetAllStudentQuery());
            IPagedList<TransactionResponse> query = (await Mediator
                .Send(new GetAllTransactionsQuery()))
                .ToPagedList(page, 5);
            return View(query);
        }
        [HttpGet]
        public IActionResult UpdateTransaction(TransactionResponse TransactionDto)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTransaction([FromForm] UpdateTransactionCommand TransactionCommand)
        {
            await Mediator.Send(TransactionCommand);
            return RedirectToAction(nameof(GetAllTransactions));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(DeleteTransactionCommand TransactionCommand)
        {
            await Mediator.Send(TransactionCommand);
            return RedirectToAction(nameof(GetAllTransactions));
        }

    }
}
