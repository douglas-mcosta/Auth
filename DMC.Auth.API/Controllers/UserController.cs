using AutoMapper;
using DMC.Auth.API.Application.Commands;
using DMC.Auth.API.Application.Queries;
using DMC.Auth.API.Application.Queries.ViewModels;
using DMC.Core.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMC.Auth.API.Controllers
{
    [Route("user")]
    public class UserController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IUserQueries _userQueries;
        private readonly IMediatorHandler _mediator;

        public UserController(IMapper mapper, IUserQueries userQueries, IMediatorHandler mediator)
        {
            _mapper = mapper;
            _userQueries = userQueries;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IList<UserViewModel>>> GetAll()
        {
            var users = await _userQueries.GetAllAsync();
            var usersVm = _mapper.Map<IList<UserViewModel>>(users);
            return CustomResponse(usersVm);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IList<UserViewModel>>> GetUserById(Guid id)
        {
            var user = await _userQueries.GetUserByIdAsync(id);
            var userVm = _mapper.Map<UserViewModel>(user);
            return CustomResponse(userVm);
        }


        [HttpPost]
        public async Task<ActionResult<UserViewModel>> RegisterUser(UserViewModel userViewModel)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var command = new RegisterUserCommand(userViewModel.Id,
                                                  userViewModel.FirstName,
                                                  userViewModel.LastName,
                                                  userViewModel.Email,
                                                  userViewModel.BirthDate,
                                                  userViewModel.Education);

            var result = await _mediator.SendCommand(command);

            if (!result.IsValid) return CustomResponse(result);

            return CustomResponse(userViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserViewModel>> UpdateUser(UpdateUserViewModel updateUserViewModel)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var userUpdate = await GetUser(updateUserViewModel.Id);

            if (userUpdate is null)
            {
                NotifierError("Usuário inválido.");
                return CustomResponse();
            }

            var command = new UpdateUserCommand(userUpdate.Id,
                                                updateUserViewModel.FirstName,
                                                updateUserViewModel.LastName,
                                                userUpdate.BirthDate,
                                                updateUserViewModel.Education);

            var result = await _mediator.SendCommand(command);

            if (!result.IsValid) return CustomResponse(result);
            userUpdate = await GetUser(updateUserViewModel.Id);
            return CustomResponse(userUpdate);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UserViewModel>> DeleteUser(Guid id)
        {
            var user = await GetUser(id);
            if(id == Guid.Empty || user is null)
            {
                NotifierError("Id inválido.");
                return CustomResponse();
            }

            var command = new DeleteUserCommand(id);
            var result = await _mediator.SendCommand(command);
            if (!result.IsValid) return CustomResponse(result);

            return CustomResponse(user);
        }

            private async Task<UserViewModel> GetUser(Guid userId)
        {
            var user = _mapper.Map<UserViewModel>(await _userQueries.GetUserByIdAsync(userId));
            return user;
        }
    }
}