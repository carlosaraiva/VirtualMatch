using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using VirtualMatch.Data;
using VirtualMatch.Data.Repositories;
using VirtualMatch.Entities;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;

namespace VirtualMatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private UserRepository _userRepository { get; set; }

        public AccountsController(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody]AccountsPostRequest request)
        {
            using (var crypto = new HMACSHA512())
            {
                var user = new User
                {
                    UserName = request.Username,
                    Password = crypto.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                    Salt = crypto.Key
                };

                await _userRepository.Insert(user);

                return user;
            }
        }

        
    }
}
