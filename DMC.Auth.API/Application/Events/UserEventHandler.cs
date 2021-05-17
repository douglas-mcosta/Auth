using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DMC.Auth.API.Application.Events
{
    public class UserEventHandler : 
        INotificationHandler<RegisteredUserEvent>,
        INotificationHandler<UpdatedUserEvent>,
        INotificationHandler<DeletedUserEvent>
    {
        public Task Handle(RegisteredUserEvent notification, CancellationToken cancellationToken)
        {
            //TODO: Enviar e-mail
            return Task.CompletedTask;
        }

        public Task Handle(UpdatedUserEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(DeletedUserEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}