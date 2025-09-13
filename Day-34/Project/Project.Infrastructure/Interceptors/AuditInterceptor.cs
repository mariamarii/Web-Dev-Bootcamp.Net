// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.ChangeTracking;
// using Microsoft.EntityFrameworkCore.Diagnostics;
// using Project.Domain.Models.Base;
//
// namespace Project.Infrastructure.Interceptors;
//
// public class AuditInterceptor(IUserContext userContext) : SaveChangesInterceptor
// {
//     public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
//         InterceptionResult<int> result, CancellationToken cancellationToken = default)
//     {
//         if (eventData.Context is null)
//         {
//             return base.SavingChangesAsync(
//                 eventData, result, cancellationToken);
//         }
//
//         foreach (EntityEntry<IAuditableEntity> entry in eventData.Context.ChangeTracker.Entries<IAuditableEntity>())
//         {
//             if (entry.State == EntityState.Added)
//             {
//                 entry.Entity.CreatedAt = DateTime.Now;
//                 entry.Entity.CreatedBy = userContext.GetUserId();
//                 entry.Entity.UpdatedAt = DateTime.Now;
//                 entry.Entity.UpdatedBy = userContext.GetUserId();
//             }
//         }
//
//         return base.SavingChangesAsync(eventData, result, cancellationToken);
//     }
// }
