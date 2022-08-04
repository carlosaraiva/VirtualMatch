using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Business.Services.Interface;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Business.Services
{
    public class LikesService : ILikesService
    {
        private readonly ILikesService _likesService;

        public LikesService(ILikesService likesService)
        {
            this._likesService = likesService;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await this._likesService.GetUserLike(sourceUserId, likedUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            return await this._likesService.GetUserLikes(predicate, userId);
        }

        public async Task<User> GetUserWithLikes(int userId)
        {
            return await this._likesService.GetUserWithLikes(userId);
        }

        public async Task AddLike(UserLike userLike)
        {
            await this._likesService.AddLike(userLike);
        }
    }
}
