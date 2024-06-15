using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P6.Application.Abstract;
using P6.Application.DTOs;
using P6.Core.Entities;
using System.Net;
using System.Security.Claims;

namespace P6.API.Controller
{
    //[Authorize(Roles ="ADMIN")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var res = await _userService.GetUserListServiceAsync();
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetailAsync(Guid id)
        {
            var res = await _userService.GetUserByIdServiceAsync(id);
            return StatusCode((int)res.StatusCode, res);
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(UserSaveDTO user)
        {
            var res = await _userService.InsertUserServiceAsync(user);
            return StatusCode((int)res.StatusCode, res);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UserSaveDTO user)
        {
            var res = await _userService.UpdateUserServiceAsync(user);
            return StatusCode((int)res.StatusCode, res);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var res = await _userService.DeleteUserServiceAsync(id);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("multiple")]
        public async Task<IActionResult> AddMultipleAsync(List<UserSaveDTO> users)
        {
            var res = await _userService.InsertMultipleUserServiceAsync(users);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("multiple")]
        public async Task<IActionResult> UpdateMultipleAsync(List<UserSaveDTO> users)
        {
            var res = await _userService.UpdateMultipleUserServiceAsync(users);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("multiple")]
        public async Task<IActionResult> DeleteMultipleAsync(List<Guid> ids)
        {
            var res = await _userService.DeleteMultipleUserServiceAsync(ids);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("import")]
        public async Task<IActionResult> Inport(List<UserSaveDTO> users)
        {
            var res = await _userService.InsertMultipleUserServiceAsync(users);
            return StatusCode((int)res.StatusCode, res);
        }

    }
}
