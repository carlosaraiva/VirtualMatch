using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMatch.Entities.Database;

namespace VirtualMatch.Business.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUsersByUsernameAsync(string username);
    }
}