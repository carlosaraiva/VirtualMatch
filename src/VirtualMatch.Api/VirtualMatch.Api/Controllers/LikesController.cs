using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VirtualMatch.Api.ActionFilters;
using VirtualMatch.Business.Interfaces;
using VirtualMatch.Business.Services;
using VirtualMatch.Business.Services.Interface;
using VirtualMatch.Data;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;
using VirtualMatch.Shared.Extensions;
using VirtualMatch.Shared.Helpers;

namespace VirtualMatch.Api.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;
        private readonly IUserService _userService;

        public LikesController(ILikesService likesService, IUserService userService)
        {
            this._likesService = likesService;
            this._userService = userService;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.FindFirst(ClaimTypes.Name)?.Value;

            var likedUser = await _userService.GetUsersByUsernameAsync(username);
            var sourceUser = await _likesService.GetUserWithLikes(Convert.ToInt32(sourceUserId));

            if (likedUser == null)
                return NotFound();

            if (sourceUser.UserName == username)
                return BadRequest("You can't like yourself");

            var userLike = await _likesService.GetUserLike(Convert.ToInt32(sourceUserId), likedUser.Id);

            if (userLike != null)
                return BadRequest("You already liked this user");

            userLike = new UserLike
            {
                SourceUserId = Convert.ToInt32(sourceUserId),
                LikedUserId = likedUser.Id
            };

            await _likesService.AddLike(userLike);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
        {
            var sourceUserId = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(await _likesService.GetUserLikes(predicate, Convert.ToInt32(sourceUserId)));
        }
    }
}
