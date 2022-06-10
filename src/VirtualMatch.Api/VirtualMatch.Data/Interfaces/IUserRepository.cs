﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CheckUserExistsBy(string username);
        Task Insert(User user);
        void Update(User user);
        Task<bool> SaveAllAsync(User user);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<MemberDto> GetMemberAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
    }
}