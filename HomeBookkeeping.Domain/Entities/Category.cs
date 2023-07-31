using HomeBookkeeping.Domain.Commons;
using HomeBookkeeping.Domain.States;

namespace HomeBookkeeping.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public ExpenseIncomeType ExpenseIncomeType { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
    }
}
