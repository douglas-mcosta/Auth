using DMC.Auth.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMC.Auth.API.Application.Queries
{
    public interface IUserQueries
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetUserByIdAsync(Guid UserId);
        Task<User> GetUserByEmailAsync(string email);
    }
}