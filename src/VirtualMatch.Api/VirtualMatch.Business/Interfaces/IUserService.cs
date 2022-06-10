using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Business.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<MemberDto>> GetUsers();
        Task<MemberDto> GetUsersByUsernameAsync(string username);
        Task<MemberDto> GetMemberAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
    }
}