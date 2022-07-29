using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VirtualMatch.Business.Interfaces;
using VirtualMatch.Business.Services;
using VirtualMatch.Data;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Entities.Database;
using VirtualMatch.Entities.DTO;
using VirtualMatch.Shared.Extensions;
using VirtualMatch.Shared.Helpers;

namespace VirtualMatch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;

        public UsersController(IUserService userService, IPhotoService photoService)
        {
            this._userService = userService;
            this._photoService = photoService;
        }

        [HttpGet()]
        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            userParams.CurrentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var users = await _userService.GetMembersAsync(userParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        
        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userService.GetMemberAsync(username);

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            memberUpdateDto.Username = username;

            if (await this._userService.UpdateMember(memberUpdateDto))
                return NoContent();

            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var photo = await this._photoService.AddPhotoAsync(file, username);

            if(photo == null)
                return BadRequest("Something went wrong while adding photos");

            return CreatedAtRoute("GetUser", new { username = username }, photo);
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto([FromRoute]int photoId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _photoService.SetMainPhoto(username, photoId))
                return NoContent();
            else
                return BadRequest("Some problem ocurred while setting the main photo.");
        }

        [HttpDelete("photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (await this._photoService.DeletePhotoAsync(username, photoId))
                return Ok();

            return BadRequest("Photo could not be deleted");
        }
    }
}
