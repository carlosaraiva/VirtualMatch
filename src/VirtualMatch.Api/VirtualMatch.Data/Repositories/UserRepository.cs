using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Data;
using VirtualMatch.Entities.Database;

namespace VirtualMatch.Data.Repositories
{
    public class UserRepository
    {
        private DataContext _dataContext { get; set; }

        public UserRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task Insert(User user)
        {
            await this._dataContext.Users.AddAsync(user);
            await this._dataContext.SaveChangesAsync();
        }

        public async Task<bool> CheckUserExistsBy(string username)
        {
            return await this._dataContext.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower());
        }

        public async Task<User> GetUserBy(string username)
        {
            return await this._dataContext.Users.FirstOrDefaultAsync(user => user.UserName.ToLower() == username.ToLower());
        }


    }
}
