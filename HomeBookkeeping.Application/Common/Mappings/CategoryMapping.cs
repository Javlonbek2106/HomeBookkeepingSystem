using AutoMapper;
using HomeBookkeeping.Application.UseCases.Categories.Commands.CreateCategory;
using HomeBookkeeping.Application.UseCases.Categories.Commands.DeleteCategory;
using HomeBookkeeping.Application.UseCases.Categories.Commands.UpdateCategory;
using HomeBookkeeping.Application.UseCases.Categorys.Response;
using HomeBookkeeping.Domain.Entities;

namespace HomeBookkeeping.Application.Common.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CategoryMappings();
        }
        private void CategoryMappings()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<DeleteCategoryCommand, Category>();
            CreateMap<Category, CategoryResponse>();
        }
    }
}
