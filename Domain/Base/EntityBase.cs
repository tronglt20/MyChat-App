using Domain.Base.Interfaces;
using MediatR;

namespace Domain.Base
{
    public abstract class EntityBase : IEntityBase
    {
        private List<INotification> _domainEvents = new List<INotification>();

        public virtual void AddDomainEvent(INotification @event)
        {
            if (_domainEvents == null)
                _domainEvents = new List<INotification>();

            _domainEvents.Add(@event);
        }

        public void ClearDomainEvent()
        {
            _domainEvents?.Clear();
        }

        public virtual IReadOnlyCollection<INotification> GetDomainEvents()
        {
            return _domainEvents;
        }
    }
}
