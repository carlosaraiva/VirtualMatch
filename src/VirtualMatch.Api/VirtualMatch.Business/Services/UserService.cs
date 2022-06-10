﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Business.Interfaces;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository { get; set; }
        private IMapper _mapper { get; set; }

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetUsers()
        {
            var users = await this._userRepository.GetUsersAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(users);
        }

        public async Task<MemberDto> GetUsersByUsernameAsync(string username)
        {
            var user = await this._userRepository.GetUserByUsernameAsync(username);
            return _mapper.Map<MemberDto>(user);
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await this._userRepository.GetMemberAsync(username);
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await this._userRepository.GetMembersAsync();
        }
    }
}
