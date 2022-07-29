using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Data;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;
using VirtualMatch.Shared.Helpers;

namespace VirtualMatch.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
     
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> CheckUserExistsBy(string username)
        {
            return await this._context.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower());
        }

        public async Task Insert(User user)
        {
            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<string> GetUserGender(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .Select(x => x.Gender).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {

            var user = await this.GetUserByUsernameAsync(userParams.CurrentUsername);

            if (string.IsNullOrWhiteSpace(userParams.Gender))
                userParams.Gender = user.Gender == "male" ? "female" : "male";

            var query = _context.Users.AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(u => u.Gender == userParams.Gender);

            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
        }

        public async Task AddPhotoAsync(Photo photo, string username)
        {
            var user = await this._context.Users.Include("Photos").FirstOrDefaultAsync(user => user.UserName == username);

            photo.IsMain = user.Photos?.Count == 0;

            user.Photos.Add(photo);
        }

        public async Task<bool> SetMainPhoto(string username, int photoId)
        {
            var user = await this.GetUserByUsernameAsync(username);

            user.Photos.FirstOrDefault(photo => photo.IsMain).IsMain = false;
            user.Photos.FirstOrDefault(photo => photo.Id == photoId).IsMain = true;

            return await this.SaveAllAsync();
        }

        public async Task<bool> DeletePhoto(string username, int photoId)
        {
            var user = await this.GetUserByUsernameAsync(username);
            user.Photos.Remove(user.Photos.FirstOrDefault(photo => photo.Id == photoId));

            return await this.SaveAllAsync();
        }

    }
}
