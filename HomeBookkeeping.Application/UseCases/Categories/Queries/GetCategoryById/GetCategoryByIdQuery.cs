using AutoMapper;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.UseCases.Categorys.Response;
using MediatR;
using Category = HomeBookkeeping.Domain.Entities.Category;

namespace HomeBookkeeping.Application.UseCases.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryResponse>;

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetCategoryByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            Category? Category = await _context.Categories.FindAsync(request.Id);

            if (Category is null)
                throw new NotFoundException(nameof(Category), request.Id);

            return _mapper.Map<CategoryResponse>(Category);
        }
    }
}
