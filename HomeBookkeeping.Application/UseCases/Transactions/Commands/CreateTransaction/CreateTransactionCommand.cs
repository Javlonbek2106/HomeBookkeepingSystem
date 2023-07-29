using AutoMapper;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Domain.Entities;
using MediatR;

namespace HomeBookkeeping.Application.UseCases.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<Guid>
    {
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public Guid CategoryId { get; set; }
    }
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateTransactionCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            Transaction Transaction = _mapper.Map<Transaction>(request);
            await _context.Transactions.AddAsync(Transaction, cancellationToken);
            await _context.SaveChangesAsync();
            return Transaction.Id;
        }
    }
}
