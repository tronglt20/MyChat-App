using Domain.Base.Interfaces;
using Domain.Interfaces.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _context;

        public UnitOfWork(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func)
        {
            if (_context.Database.CurrentTransaction == null)
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                var transResult = await strategy.ExecuteAsync(async () =>
                {
                    using (var trans = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            var result = await func.Invoke();
                            await trans.CommitAsync();
                            return result;
                        }
                        catch (Exception)
                        {
                            await trans.RollbackAsync();
                            throw;
                        }
                    }
                });

                return transResult;
            }
            else
                return await func.Invoke();
        }

        public virtual IBaseRepository<T> Repository<T>() where T : class
        {
            return _serviceProvider.GetService<IBaseRepository<T>>();
        }

        #region Save changes
        public async Task<int> SaveChangesAsync()
        {
            var entries = _context.ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        OnEntryAdded(entry);
                        break;

                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        OnEntryUpdated(entry);
                        break;

                    case Microsoft.EntityFrameworkCore.EntityState.Detached:
                    case Microsoft.EntityFrameworkCore.EntityState.Unchanged:
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        break;
                }
            }

            var domainEvents = GetDomainEvents();

            int saved = await _context.SaveChangesAsync();

            // Push domain events
            if (domainEvents?.Count() > 0)
                await PushDomainEventsAsync(domainEvents);

            return saved;
        }

        private void OnEntryUpdated(EntityEntry entry)
        {
            throw new NotImplementedException();
        }

        private void OnEntryAdded(EntityEntry entry)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Domain events
        private async Task PushDomainEventsAsync(IEnumerable<INotification> @events)
        {
            var mediator = _serviceProvider.GetService<IMediator>();

            if (mediator != null)
            {
                foreach (var @event in @events)
                {
                    await mediator.Publish(@event);
                }
            }
        }

        private IEnumerable<INotification> GetDomainEvents()
        {
            var entries = _context.ChangeTracker.Entries();

            var events = entries
                .Where(_ =>
                    typeof(IEntityBase).IsAssignableFrom(_.Entity.GetType())
                    && (_.Entity as IEntityBase)?.GetDomainEvents().Any() == true
                ).SelectMany(_ =>
                {
                    var entity = (_.Entity as IEntityBase);
                    var events = entity?.GetDomainEvents().ToList();
                    entity?.ClearDomainEvent();
                    return events;
                })
                .ToList();

            return events;
        }
        #endregion

    }
}
