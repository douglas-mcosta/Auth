using DMC.Core.Messages;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace DMC.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}