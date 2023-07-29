using System.ComponentModel.DataAnnotations.Schema;
using HomeBookkeeping.Domain.Commons;

namespace HomeBookkeeping.Domain.Entities
{
    public class Transaction : BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
