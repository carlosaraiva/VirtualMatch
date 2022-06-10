using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Data;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Entities.Database;

namespace VirtualMatch.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context { get; set; }

        public UserRepository(DataContext dataContext)
        {
            this._context = dataContext;
        }

        public async Task<bool> CheckUserExistsBy(string username)
        {
            return await this._context.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower());
        }

        public async Task Insert(User user)
        {
            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<string> GetUserGender(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .Select(x => x.Gender).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync(User user)
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
