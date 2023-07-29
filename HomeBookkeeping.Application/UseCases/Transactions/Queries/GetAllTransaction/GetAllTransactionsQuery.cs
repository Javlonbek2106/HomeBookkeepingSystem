using AutoMapper;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using HomeBookkeeping.Domain.Entities;
using MediatR;

namespace HomeBookkeeping.Application.UseCases.Transactions.Queries.GetAllTransactions
{
    public record GetAllTransactionsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<IEnumerable<TransactionResponse>>;

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, IEnumerable<TransactionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllTransactionsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<TransactionResponse>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Transaction> Transactions = _context.Transactions;

            return await Task.FromResult(_mapper.Map<IEnumerable<TransactionResponse>>(Transactions));
        }
    }
}
