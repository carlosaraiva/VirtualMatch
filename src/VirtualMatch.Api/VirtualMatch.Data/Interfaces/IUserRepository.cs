﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;
using VirtualMatch.Shared.Helpers;

namespace VirtualMatch.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CheckUserExistsBy(string username);
        Task Insert(User user);
        void Update(User user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<MemberDto> GetMemberAsync(string username);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task AddPhotoAsync(Photo photo, string username);
        Task<bool> SetMainPhoto(string username, int photoId);
        Task<bool> DeletePhoto(string username, int photoId);
    }
}