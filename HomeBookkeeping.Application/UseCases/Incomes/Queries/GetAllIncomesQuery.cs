using AutoMapper;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using HomeBookkeeping.Domain.Entities;
using MediatR;

namespace HomeBookkeeping.Application.UseCases.Incomes.Queries
{
    public record GetAllIncomesQuery : IRequest<IEnumerable<TransactionResponse>>; //kirim
    public class GetAllIncomesQueryHandler : IRequestHandler<GetAllIncomesQuery, IEnumerable<TransactionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllIncomesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IEnumerable<TransactionResponse>> Handle(GetAllIncomesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Category> Categories = _context.Categories;
            IEnumerable<Transaction> Transactions = _context.Transactions.Where(t => t.Category.ExpenseIncomeType == Domain.States.ExpenseIncomeType.доход); 
            return await Task.FromResult(_mapper.Map<IEnumerable<TransactionResponse>>(Transactions));
        }
    }
}
