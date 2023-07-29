using HomeBookkeeping.Domain.Commons;

namespace HomeBookkeeping.Domain.Entities
{
    public class Transaction : BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public string? Comment { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
