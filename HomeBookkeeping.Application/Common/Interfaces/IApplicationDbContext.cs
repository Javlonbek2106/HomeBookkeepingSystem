using HomeBookkeeping.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeBookkeeping.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Transaction> Transactions { get; }
    DbSet<Category> Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
