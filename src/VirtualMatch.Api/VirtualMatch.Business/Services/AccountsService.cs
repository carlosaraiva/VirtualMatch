using System;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Data.Repositories;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Business.Services
{
    public class AccountsService
    {
        private UserRepository _userRepository { get; set; }
        private TokenService _tokenService { get; set; }

        public AccountsService(UserRepository userRepository, TokenService tokenService)
        {
            this._userRepository = userRepository;
            this._tokenService = tokenService;
        }

        public async Task<AccountsPostResponse> Register(AccountsPostRequest request)
        {
            if (await this._userRepository.CheckUserExistsBy(request.Username))
                throw new ArgumentException("Username already exists.");

            using (var crypto = new HMACSHA512())
            {
                var user = new User
                {
                    UserName = request.Username,
                    Password = crypto.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                    Salt = crypto.Key
                };

                await _userRepository.Insert(user);

                return new AccountsPostResponse { 
                    Id = user.Id,
                    UserName = user.UserName
                };
            }
        }

        public async Task<LoginPostResponse> Login(LoginPostRequest request)
        {
            var user = await _userRepository.GetUserBy(request.Username);

            if (user is null)
                throw new AuthenticationException("Invalid user or password.");

            using (var crypto = new HMACSHA512(user.Salt))
            {
                var computedPassword = crypto.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

                for(int i = 0; i < user.Password.Length; ++i)
                {
                    if (computedPassword[i] != user.Password[i])
                        throw new AuthenticationException("Invalid user or password.");
                }

                return new LoginPostResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = this._tokenService.Create(user)
                };
            }

            
        }
    }
}
