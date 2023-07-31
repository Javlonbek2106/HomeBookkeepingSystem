using AutoMapper;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using HomeBookkeeping.Domain.Entities;
using MediatR;

namespace HomeBookkeeping.Application.UseCases.Expenses.Queries
{
    public record GetAllExpensesQuery : IRequest<IEnumerable<TransactionResponse>>; //rasxod
    public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, IEnumerable<TransactionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllExpensesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IEnumerable<TransactionResponse>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Transaction> Transactions = _context.Transactions.Where(t=>t.Category.ExpenseIncomeType==Domain.States.ExpenseIncomeType.расход);
            return await Task.FromResult(_mapper.Map<IEnumerable<TransactionResponse>>(Transactions));
        }
    }
}
