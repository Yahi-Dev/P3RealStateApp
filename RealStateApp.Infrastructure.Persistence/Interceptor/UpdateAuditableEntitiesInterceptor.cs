using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Infrastructure.Persistence.Interceptor
{
    public sealed class UpdateAuditableEntitiesInterceptor
        : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;
            if (dbContext == null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }
            var entries = dbContext.ChangeTracker
                .Entries<IAuditableEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(a => a.CreatedDate).CurrentValue = DateTime.UtcNow;
                    entry.Property(a => a.CreatedById).CurrentValue = "Default";


                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(a => a.LastModifiedDate).CurrentValue = DateTime.UtcNow;
                    entry.Property(a => a.LastModifiedById).CurrentValue = "Default";
                }
                if (entry.State == EntityState.Deleted)
                {
                    entry.Property(a => a.DeletedDate).CurrentValue = DateTime.UtcNow;
                    entry.Property(a => a.DeletedById).CurrentValue = "Default";
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
