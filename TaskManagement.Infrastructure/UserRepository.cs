using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagementDbContext _context;
        public UserRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
                var result = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return result.Entity;
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users;
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = _context.Update(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UserExists(long id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }
    }

}
