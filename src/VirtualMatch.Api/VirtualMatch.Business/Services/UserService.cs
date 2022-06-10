using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Business.Interfaces;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Entities.Database;

namespace VirtualMatch.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this._userRepository.GetUsersAsync();
        }

        public async Task<User> GetUsersByUsernameAsync(string username)
        {
            return await this._userRepository.GetUserByUsernameAsync(username);
        }
    }
}
