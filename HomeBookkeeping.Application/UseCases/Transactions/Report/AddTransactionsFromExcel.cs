using AutoMapper;
using ClosedXML.Excel;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Transaction = HomeBookkeeping.Domain.Entities.Transaction;

namespace HomeBookkeeping.Application.UseCases.Transactions.Report;
//public record AddTransactionsFromExcel(IFormFile ExcelFile) : IRequest<List<TransactionResponse>>;

//public class AddTransactionsFromExcelHandler : IRequestHandler<AddTransactionsFromExcel, List<TransactionResponse>>
//{

//    private readonly IApplicationDbContext _context;
//    private readonly IMapper _mapper;

//    public AddTransactionsFromExcelHandler(IApplicationDbContext context, IMapper mapper)
//    {

//        _context = context;
//        _mapper = mapper;
//    }

//    public async Task<List<TransactionResponse>> Handle(AddTransactionsFromExcel request, CancellationToken cancellationToken)
//    {
//        if (request.ExcelFile == null || request.ExcelFile.Length == 0)
//            throw new ArgumentNullException("File", "file is empty or null");

//        var file = request.ExcelFile;
//        List<Transaction> result = new();
//        using (var ms = new MemoryStream())
//        {

//            await file.CopyToAsync(ms, cancellationToken);
//            using (var wb = new XLWorkbook(ms))
//            {
//                var sheet1 = wb.Worksheet(1);
//                int startRow = 2;
//                for (int row = startRow; row <= sheet1.LastRowUsed().RowNumber(); row++)
//                {
//                    var Transaction = new Transaction()
//                    {
//                        Amount = decimal.Parse(sheet1.Cell(row, 2).GetString()),
//                        Comment = sheet1.Cell(row, 3).GetString(),
//                        CreatedAt = DateTime.Parse(sheet1.Cell(row, 4).GetString()),
//                        CategoryId = Guid.Parse(sheet1.Cell(row, 5).GetString()),
//                    };

//                    result.Add(Transaction);
//                }
//            }
//        }
//        await _context.Transactions.AddRangeAsync(result);
//        await _context.SaveChangesAsync();
//        return _mapper.Map<List<TransactionResponse>>(result);
//    }
//}
