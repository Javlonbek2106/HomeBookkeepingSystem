using AutoMapper;
using ClosedXML.Excel;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.UseCases.Categorys.Response;
using HomeBookkeeping.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HomeBookkeeping.Application.UseCases.Categories.Import.Export;

//public record AddCategoriesFromExcel(IFormFile ExcelFile) : IRequest<List<CategoryResponse>>;

//public class AddCategoriesFromExcelHandler : IRequestHandler<AddCategoriesFromExcel, List<CategoryResponse>>
//{

//    private readonly IApplicationDbContext _context;
//    private readonly IMapper _mapper;

//    public AddCategoriesFromExcelHandler(IApplicationDbContext context, IMapper mapper)
//    {

//        _context = context;
//        _mapper = mapper;
//    }

//    public async Task<List<CategoryResponse>> Handle(AddCategoriesFromExcel request, CancellationToken cancellationToken)
//    {
//        if (request.ExcelFile == null || request.ExcelFile.Length == 0)
//            throw new ArgumentNullException("File", "file is empty or null");

//        var file = request.ExcelFile;
//        List<Category> result = new();
//        using (var ms = new MemoryStream())
//        {

//            await file.CopyToAsync(ms, cancellationToken);
//            using (var wb = new XLWorkbook(ms))
//            {
//                var sheet1 = wb.Worksheet(1);
//                int startRow = 2;
//                for (int row = startRow; row <= sheet1.LastRowUsed().RowNumber(); row++)
//                {
//                    var Category = new Category()
//                    {
//                        ExpenceIncomeType = sheet1.Cell(row, 2).GetString(),
//                        CategoryName = sheet1.Cell(row, 3).GetString(),
//                    };
//                }
//            }
//        }
//        await _context.Categories.AddRangeAsync(result);
//        await _context.SaveChangesAsync();
//        return _mapper.Map<List<CategoryResponse>>(result);
//    }
//}
                        