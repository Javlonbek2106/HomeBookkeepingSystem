using System.Data;
using AutoMapper;
using ClosedXML.Excel;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.Common.Models;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MarketManager.Application.UseCases.Transactions.Import.Export
{
    public class GetTransactionExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetTransactionExcelHandler : IRequestHandler<GetTransactionExcel, ExcelReportResponse>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTransactionExcelHandler(IApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelReportResponse> Handle(GetTransactionExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook wb = new())
            {
                var TransactionData = await GetTransactionAsync(cancellationToken);
                var sheet1 = wb.AddWorksheet(TransactionData, "Transactions");


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

        private async Task<DataTable> GetTransactionAsync(CancellationToken cancellationToken = default)
        {
            var AllTransactions = await _context.Categories.ToListAsync(cancellationToken);

            DataTable dt = new()
            {
                TableName = "Empdata"
            };
            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("Comment", typeof(string));
            dt.Columns.Add("CreatedAt", typeof(DateTime));
            dt.Columns.Add("CategoryId", typeof(Guid));


            var _list = _mapper.Map<List<TransactionResponse>>(AllTransactions);
            if (_list.Count > 0)
            {
                _list.ForEach(Transaction =>
                {
                    dt.Rows.Add(Transaction.Id, Transaction.Amount, Transaction.Comment, Transaction.CreatedAt, Transaction.CategoryId);

                });
            }

            return dt;
        }

    }
}

