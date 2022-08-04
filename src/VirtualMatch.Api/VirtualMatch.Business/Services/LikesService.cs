using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Business.Services.Interface;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Business.Services
{
    public class LikesService : ILikesService
    {
        private readonly ILikesRepository _likesRepository;

        public LikesService(ILikesRepository likesRepository)
        {
            this._likesRepository = likesRepository;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await this._likesRepository.GetUserLike(sourceUserId, likedUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            return await this._likesRepository.GetUserLikes(predicate, userId);
        }

        public async Task<User> GetUserWithLikes(int userId)
        {
            return await this._likesRepository.GetUserWithLikes(userId);
        }

        public async Task AddLike(UserLike userLike)
        {
            await this._likesRepository.AddLike(userLike);
        }
    }
}
