using HomeBookkeeping.Application.UseCases.Incomes.Queries;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolMonitoringSystem.Api.Controllers;
using X.PagedList;

namespace HomeBookkeeping.Controllers
{
    public class IncomeController : ApiBaseController
    {
        [HttpGet]
        [EnableRateLimiting("Token")]
        public async Task<IActionResult> GetAllIncomes(int page = 1)
        {
            //ViewData["students"] = await Mediator.Send(new GetAllStudentQuery());
            IPagedList<TransactionResponse> query = (await Mediator
                .Send(new GetAllIncomesQuery()))
                .ToPagedList(page, 5);
            return View(query);
        }
    }
}
