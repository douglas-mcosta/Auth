using DMC.Core.Messages;
using System;

namespace DMC.Auth.API.Application.Events
{
    public class UpdatedUserEvent : Event
    {
        public Guid Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }

        public UpdatedUserEvent(Guid id, string firstName, string lastName, DateTime birthDate)
        {
            AggregateId = id;
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

    }
}
