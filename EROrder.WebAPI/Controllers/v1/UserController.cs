using Microsoft.AspNetCore.Mvc;
using EROrder.Shared.Dtos;
using EROrder.Shared.Dtos.Request.User;
using EROrder.Shared.Dtos.Response.User;
using EROrder.Shared.Enums;
using EROrder.Shared.Helpers;
using EROrder.WebAPI.Controllers.Common;
using EROrder.Core.Services.Interfaces;

namespace EROrder.WebAPI.Controllers.v1
{
    [Route($"{MagicHelper.ApiRoutes.BaseApi}/{MagicHelper.ApiVersions.V1}/[controller]")]
    public class UserController(IUserService userService) : BaseApiController
    {
        private readonly IUserService userService = userService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UserResponseDto>>>> GetUsers()
        {
            var result = await userService.GetUsersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetUser(int id)
        {
            var result = await userService.GetUserByIdAsync(id);
            
            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> CreateUser([FromBody] CreateUserRequestDto request)
        {
            var result = await userService.CreateUserAsync(request.Name, request.Email);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(nameof(GetUser), new { id = result.Data?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> UpdateUser(int id, [FromBody] UpdateUserRequestDto request)
        {
            var result = await userService.UpdateUserAsync(id, request.Name, request.Email, request.IsActive);
            
            if (!result.Success)
            {
                return result.Status == API_STATUS.NOT_FOUND ? NotFound(result) : BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteUser(int id)
        {
            var result = await userService.DeleteUserAsync(id);
            
            if (!result.Success)
            {
                return result.Status == API_STATUS.NOT_FOUND ? NotFound(result) : BadRequest(result);
            }

            return Ok(result);
        }
    }
}
