using AutoMapper;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.UseCases.Categorys.Response;
using HomeBookkeeping.Domain.Entities;
using MediatR;

namespace HomeBookkeeping.Application.UseCases.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery(int PageNumber = 1, int PageSize = 10) : IRequest<IEnumerable<CategoryResponse>>;

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllCategoriesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Category> Categories = _context.Categories;

            return await Task.FromResult(_mapper.Map<IEnumerable<CategoryResponse>>(Categories));
        }
    }
}
