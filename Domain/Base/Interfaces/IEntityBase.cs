using MediatR;

namespace Domain.Base.Interfaces
{
    public interface IEntityBase
    {
        void AddDomainEvent(INotification @event);

        void ClearDomainEvent();

        IReadOnlyCollection<INotification> GetDomainEvents();
    }
}
