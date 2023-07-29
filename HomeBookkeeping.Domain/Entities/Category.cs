using HomeBookkeeping.Domain.Commons;

namespace HomeBookkeeping.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public Guid CategoryId { get; set; }
        public string ExpenceIncomeType { get; set; }
        public string CategoryName { get; set; }
        public virtual IList<Transaction>? Transactions { get; set; } = new List<Transaction>();
    }
}
