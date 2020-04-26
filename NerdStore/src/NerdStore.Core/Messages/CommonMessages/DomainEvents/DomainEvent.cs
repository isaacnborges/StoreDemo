using MediatR;
using NerdStore.Core.Messages;
using System;

namespace NerdStore.Core.CommonMessages.DomainEvents
{
    public abstract class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent(Guid aggregateId)
        {
            Timestamp = DateTime.Now;
            AggregateId = aggregateId;
        }
    }
}
