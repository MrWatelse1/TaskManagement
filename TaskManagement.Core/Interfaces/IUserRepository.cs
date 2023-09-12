using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        Task<User> GetUser(long id);
        Task<User> GetUserByEmail(string email);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(User user);
        Task<bool> UserExists(long id);
    }
}
