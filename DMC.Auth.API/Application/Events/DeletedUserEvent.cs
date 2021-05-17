using DMC.Core.Messages;
using System;

namespace DMC.Auth.API.Application.Events
{
    public class DeletedUserEvent : Event
    {
        public DeletedUserEvent(Guid id, string email, string firstName)
        {
            AggregateId = id;
            Id = id;
            Email = email;
            FirstName = firstName;
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
    }
}