using DMC.Auth.API.Models;
using DMC.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMC.Auth.API.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetUserByIdAsync(Guid UserId);
        Task<User> GetUserByEmailAsync(string email);

        Task Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}