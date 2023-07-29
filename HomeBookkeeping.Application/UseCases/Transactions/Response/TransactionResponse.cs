namespace HomeBookkeeping.Application.UseCases.Transactions.Response;

public class TransactionResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CategoryId { get; set; }
}
