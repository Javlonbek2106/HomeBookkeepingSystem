using AutoMapper;
using HomeBookkeeping.Application.Common.Exceptions;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using HomeBookkeeping.Domain.Entities;
using MediatR;

namespace HomeBookkeeping.Application.UseCases.Transactions.Queries.GetTransactionById
{
    public record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionResponse>;

    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetTransactionByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<TransactionResponse> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            Transaction? Transaction = await _context.Transactions.FindAsync(request.Id);

            if (Transaction is null)
                throw new NotFoundException(nameof(Transaction), request.Id);

            return _mapper.Map<TransactionResponse>(Transaction);
        }
    }
}
