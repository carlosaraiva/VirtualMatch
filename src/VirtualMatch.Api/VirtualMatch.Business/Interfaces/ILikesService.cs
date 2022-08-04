using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Business.Services.Interface
{
    public interface ILikesService
    {
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
        Task<User> GetUserWithLikes(int userId);
        Task AddLike(UserLike userLike);
    }
}