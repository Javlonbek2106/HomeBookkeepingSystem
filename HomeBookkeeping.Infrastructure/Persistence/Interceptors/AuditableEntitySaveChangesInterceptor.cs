﻿using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HomeBookkeeping.Infrastructure.Persistence.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        //private readonly ICurrentUserService _currentUserService;

        public AuditableEntitySaveChangesInterceptor(/*ICurrentUserService currentUserService*/)
        {
            //_currentUserService = currentUserService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    //entry.Entity.CreatedBy = _currentUserService.Username;
                    entry.Entity.CreatedDate = DateTime.Now;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    //entry.Entity.ModifyBy = _currentUserService.Username;
                    entry.Entity.ModifyDate = DateTime.Now;
                }
            }
        }
    }
}
