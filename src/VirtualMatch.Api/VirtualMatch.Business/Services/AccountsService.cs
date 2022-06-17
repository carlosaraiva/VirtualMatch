using AutoMapper;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Data.Repositories;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Business.Services
{
    public class AccountsService
    {
        private IUserRepository _userRepository { get; set; }
        private TokenService _tokenService { get; set; }

        private IMapper _mapper { get; set; }


        public AccountsService(IUserRepository userRepository, TokenService tokenService, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._tokenService = tokenService;
            this._mapper = mapper;
        }

        public async Task<AccountsPostResponse> Register(AccountsPostRequest request)
        {
            if (await this._userRepository.CheckUserExistsBy(request.Username))
                return null;

            var user = _mapper.Map<User>(request);

            using (var crypto = new HMACSHA512())
            {
                user.UserName = request.Username;
                user.Password = crypto.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
                user.Salt = crypto.Key;

                await _userRepository.Insert(user);

                return new AccountsPostResponse {
                    Id = user.Id,
                    Username = user.UserName,
                    KnownAs = user.KnownAs
                };
            }
        }

        public async Task<LoginPostResponse> Login(LoginPostRequest request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);

            if (user is null)
                return null;

            using (var crypto = new HMACSHA512(user.Salt))
            {
                var computedPassword = crypto.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

                for(int i = 0; i < user.Password.Length; ++i)
                {
                    if (computedPassword[i] != user.Password[i])
                        return null;
                }

                return new LoginPostResponse
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = this._tokenService.Create(user),
                    PhotoUrl = user.Photos.FirstOrDefault(user => user.IsMain)?.Url,
                    KnownAs = user.KnownAs
                };
            }

            
        }
    }
}
