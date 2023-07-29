using AutoMapper;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Domain.Entities;
using MediatR;
using Transaction = HomeBookkeeping.Domain.Entities.Transaction;

namespace HomeBookkeeping.Application.UseCases.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommand : IRequest
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public Guid CategoryId { get; set; }
    }
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateTransactionCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            Transaction? Transaction = await _context.Transactions.FindAsync(request.Id);
            _mapper.Map(request, Transaction);
            if (Transaction is null)
                throw new NotFoundException(nameof(Transaction), request.Id);
            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category is null)
                throw new NotFoundException(nameof(Category), request.CategoryId);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
