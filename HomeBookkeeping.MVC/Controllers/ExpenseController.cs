using HomeBookkeeping.Api.Filters;
using HomeBookkeeping.Application.UseCases.Categories.Queries.GetAllCategories;
using HomeBookkeeping.Application.UseCases.Expenses.Queries;
using HomeBookkeeping.Application.UseCases.Transactions.Commands.CreateTransaction;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using Microsoft.AspNetCore.Mvc;
using SchoolMonitoringSystem.Api.Controllers;
using X.PagedList;

namespace HomeBookkeeping.Controllers
{
    public class ExpenseController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> CreateTransaction()
        {
            ViewData["categories"] = await Mediator.Send(new GetAllCategoriesQuery());
            return View();
        }
        [HttpPost]
        [ActionModelValidationAttribute]
        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand TransactionCommand)
        {
            var result = await Mediator.Send(TransactionCommand);
            return RedirectToAction(nameof(GetAllExpenses));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses(int page = 1)
        {
            //ViewData["students"] = await Mediator.Send(new GetAllStudentQuery());
            IPagedList<TransactionResponse> query = (await Mediator
                .Send(new GetAllExpensesQuery()))
                .ToPagedList(page, 5);
            return View(query);
        }
    }
}
