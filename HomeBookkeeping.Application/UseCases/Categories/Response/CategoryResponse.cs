using HomeBookkeeping.Application.UseCases.Transactions.Response;

namespace HomeBookkeeping.Application.UseCases.Categorys.Response;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string ExpenceIncomeType { get; set; }
    public string CategoryName { get; set; }
    public virtual ICollection<TransactionResponse>? Transactions { get; set; }
}
