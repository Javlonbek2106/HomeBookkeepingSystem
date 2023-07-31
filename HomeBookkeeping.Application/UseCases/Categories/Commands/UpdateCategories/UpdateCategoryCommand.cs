using AutoMapper;
using HomeBookkeeping.Application.Common.Exceptions;
using HomeBookkeeping.Application.Common.Interfaces;
using MediatR;
using Category = HomeBookkeeping.Domain.Entities.Category;

namespace HomeBookkeeping.Application.UseCases.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
        public string ExpenceIncomeType { get; set; }
        public string CategoryName { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? Category = await _context.Categories.FindAsync(request.Id);
            _mapper.Map(request, Category);
            if (Category is null)
                throw new NotFoundException(nameof(Category), request.Id);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
