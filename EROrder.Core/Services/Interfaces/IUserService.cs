using System.Runtime.CompilerServices;
using EROrder.Shared.Dtos;
using EROrder.Shared.Dtos.Response.User;

namespace EROrder.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<List<UserResponseDto>>> GetUsersAsync([CallerMemberName] string methodName = "");
        Task<ApiResponse<UserResponseDto>> GetUserByIdAsync(int id, [CallerMemberName] string methodName = "");
        Task<ApiResponse<UserResponseDto>> CreateUserAsync(string name, string email, [CallerMemberName] string methodName = "");
        Task<ApiResponse<UserResponseDto>> UpdateUserAsync(int id, string name, string email, bool isActive, [CallerMemberName] string methodName = "");
        Task<ApiResponse<object>> DeleteUserAsync(int id, [CallerMemberName] string methodName = "");
    }
}
