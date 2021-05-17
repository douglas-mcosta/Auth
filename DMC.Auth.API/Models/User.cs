using DMC.Core.DomainObject;
using DMC.Core.DomainObject.Enum;
using System;

namespace DMC.Auth.API.Models
{
    public class User : Entity, IAggregateRoot
    {
        private User() { }
        public User(Guid id, string firstName, string lastName, string email, DateTime birthDate, int education)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = new Email(email);
            BirthDate = new BirthDate(birthDate);
            Education = (Education)education;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get => $"{FirstName} {LastName}"; private set { } }
        public Email Email { get; private set; }
        public BirthDate BirthDate { get; private set; }
        public Education Education { get; private set; }
        public void ChangeEmail(string email)
        {
            Email = new Email(email);
        }

    }
}