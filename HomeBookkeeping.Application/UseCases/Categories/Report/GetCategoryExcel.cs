using System.Data;
using AutoMapper;
using ClosedXML.Excel;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.Common.Models;
using HomeBookkeeping.Application.UseCases.Categorys.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBookkeeping.Application.UseCases.Categories.Import.Export
{
    public class GetCategoryExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetCategoryExcelHandler : IRequestHandler<GetCategoryExcel, ExcelReportResponse>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryExcelHandler(IApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelReportResponse> Handle(GetCategoryExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook wb = new())
            {
                var CategoryData = await GetCategoryAsync(cancellationToken);
                var sheet1 = wb.AddWorksheet(CategoryData, "Categories");


                sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

                sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

                sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;

                sheet1.Row(1).Style.Font.FontColor = XLColor.White;

                sheet1.Row(1).Style.Font.Bold = true;
                sheet1.Row(1).Style.Font.Shadow = true;
                sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
                sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
                sheet1.Row(1).Style.Font.Italic = true;

                sheet1.RowHeight = 20;
                sheet1.Column(1).Width = 38;
                sheet1.Column(2).Width = 20;
                sheet1.Column(3).Width = 20;
                sheet1.Column(4).Width = 20;
                sheet1.Column(5).Width = 38;



                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return new ExcelReportResponse(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");

                }
            }
        }

        private async Task<DataTable> GetCategoryAsync(CancellationToken cancellationToken = default)
        {
            var AllCategories = await _context.Categories.ToListAsync(cancellationToken);

            DataTable dt = new()
            {
                TableName = "Empdata"
            };
            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("ExpenceIncomeType", typeof(string));
            dt.Columns.Add("CategoryName", typeof(string));


            var _list = _mapper.Map<List<CategoryResponse>>(AllCategories);
            if (_list.Count > 0)
            {
                _list.ForEach(Category =>
                {
                    dt.Rows.Add(Category.Id, Category.ExpenceIncomeType, Category.CategoryName);

                });
            }

            return dt;
        }

    }
}

