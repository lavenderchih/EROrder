using EROrder.Shared.Dtos;
using EROrder.Shared.Dtos.Request.Home;
using EROrder.Shared.Dtos.Response.Station;
using System.Runtime.CompilerServices;

namespace EROrder.Core.Services.Interfaces
{
    public interface IHomeService
    {
        Task<ApiResponse<List<HomeResponseDto>>> GetNursingStationsAsync();
        Task<ApiResponse<List<ERempResponseDto>>> GetEREmpAsync();
        Task<ApiResponse<List<ERPatientResponseDto>>> GetERPatientAsync(ERPatientListRequestDto request);
        //Task<ApiResponse<List<UserResponseDto>>> GetUsersAsync([CallerMemberName] string methodName = "");
        //Task<ApiResponse<UserResponseDto>> GetUserByIdAsync(int id, [CallerMemberName] string methodName = "");
        //Task<ApiResponse<UserResponseDto>> CreateUserAsync(string name, string email, [CallerMemberName] string methodName = "");
        //Task<ApiResponse<UserResponseDto>> UpdateUserAsync(int id, string name, string email, bool isActive, [CallerMemberName] string methodName = "");
        //Task<ApiResponse<object>> DeleteUserAsync(int id, [CallerMemberName] string methodName = "");
    }
}
