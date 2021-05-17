using DMC.Core.Messages;
using System;

namespace DMC.Auth.API.Application.Commands
{
    public class DeleteUserCommand : Command
    {
        public Guid Id { get; private set; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}