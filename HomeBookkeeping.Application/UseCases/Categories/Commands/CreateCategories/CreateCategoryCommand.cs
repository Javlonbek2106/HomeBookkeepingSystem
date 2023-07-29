using AutoMapper;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Domain.Entities;
using MediatR;

namespace HomeBookkeeping.Application.UseCases.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string ExpenceIncomeType { get; set; }
        public string CategoryName { get; set; }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category Category = _mapper.Map<Category>(request);
            await _context.Categories.AddAsync(Category, cancellationToken);
            await _context.SaveChangesAsync();
            return Category.Id;
        }
    }
}
