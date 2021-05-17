using DMC.Core.Messages;
using System;
using System.Collections.Generic;

namespace DMC.Auth.API.Models
{
    public abstract class Entity
    {
        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public void AddEvent(Event eventItem)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(eventItem);
        }

        public void RemoveEvent(Event eventItem)
        {
            _notifications.Remove(eventItem);
        }
        public void ClearEvent()
        {
            _notifications?.Clear();
        }

    }
}