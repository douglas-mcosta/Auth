using DMC.Auth.API.Application.Events;
using DMC.Auth.API.Data.Repository;
using DMC.Auth.API.Models;
using DMC.Core.Messages;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DMC.Auth.API.Application.Commands
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<RegisterUserCommand, ValidationResult>,
        IRequestHandler<UpdateUserCommand, ValidationResult>,
        IRequestHandler<DeleteUserCommand, ValidationResult>

    {
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> Handle(RegisterUserCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var user = new User(message.Id, message.FirstName, message.LastName, message.Email, message.BirthDate, message.Education);

            var userExist = await  _userRepository.GetUserByEmailAsync(message.Email) != null;

            if (userExist)
            {
                AddError("Já existe um usuário com esse E-mail cadastrado.");
                return ValidationResult;
            }

            await _userRepository.Add(user);
            user.AddEvent(new RegisteredUserEvent(user.FirstName, user.LastName, user.Email.Address, user.BirthDate.Date));

            return await SaveChanges(_userRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateUserCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var user = await _userRepository.GetUserByIdAsync(message.Id);

            if (user is null)
            {
                AddError("Usuário inválido.");
                return ValidationResult;
            }

            var userUpdate = new User(user.Id,
                                      message.FirstName,
                                      message.LastName,
                                      user.Email.Address,
                                      message.BirthDate,
                                      message.Education);

             _userRepository.Update(userUpdate);
            user.AddEvent(new UpdatedUserEvent(user.Id, user.FirstName, user.LastName, user.BirthDate.Date));

            return await SaveChanges(_userRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DeleteUserCommand message, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(message.Id);

            if (user is null)
            {
                AddError("Usuário inválido.");
                return ValidationResult;
            }

           _userRepository.Delete(user);

            user.AddEvent(new DeletedUserEvent(user.Id, user.Email.Address, user.FirstName));

            return await SaveChanges(_userRepository.UnitOfWork);
        }
    }
}