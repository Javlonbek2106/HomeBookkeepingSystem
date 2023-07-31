using HomeBookkeeping.Application.Common.Exceptions;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Domain.Entities;
using MediatR;

namespace HomeBookkeeping.Application.UseCases.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            Transaction? Transaction = await _context.Transactions.FindAsync(request.Id);
            if (Transaction is null)
                throw new NotFoundException(nameof(Transaction), request.Id);
        }
    }
}
