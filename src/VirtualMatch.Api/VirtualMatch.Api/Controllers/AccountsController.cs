using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using VirtualMatch.Business.Services;
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
        private AccountsService _accountService { get; set; }

        public AccountsController(AccountsService accountsService)
        {
            this._accountService = accountsService;
        }

        [HttpPost]
        public async Task<ActionResult<AccountsPostResponse>> Post([FromBody]AccountsPostRequest request)
        {
            using (var crypto = new HMACSHA512())
            {
                var response = await this._accountService.Register(request);

                return response;
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginPostResponse>> PostLogin([FromBody] LoginPostRequest request)
        {
            var response = await this._accountService.Login(request);
            return Ok(response);
        }
    }
}
