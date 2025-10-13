using Serilog;
using System.Runtime.CompilerServices;
using EROrder.Core.Services.Interfaces;
using EROrder.Shared.Dtos;
using EROrder.Shared.Dtos.Response.User;
using EROrder.Shared.Enums;

namespace EROrder.Core.Services
{
    public class UserService : IUserService
    {
        private static readonly List<UserResponseDto> Users =
        [
            new() 
            {
                Id = 1,
                Name = "張小明",
                Email = "ming@example.com",
                CreatedAt = DateTime.Now.AddDays(-30),
                IsActive = true
            },
            new() 
            {
                Id = 2,
                Name = "李小華",
                Email = "hua@example.com",
                CreatedAt = DateTime.Now.AddDays(-15),
                IsActive = true
            },
            new() 
            {
                Id = 3,
                Name = "王小美",
                Email = "mei@example.com",
                CreatedAt = DateTime.Now.AddDays(-5),
                IsActive = false
            }
        ];

        public async Task<ApiResponse<List<UserResponseDto>>> GetUsersAsync([CallerMemberName] string methodName = "")
        {
            try
            {
                await Task.Yield(); // 模擬非同步操作

                return new ApiResponse<List<UserResponseDto>>
                {
                    Success = true,
                    Status = API_STATUS.SUCCESS,
                    Data = Users
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[{methodName}] 取得使用者列表時發生錯誤 {ex.Message}");
                return new ApiResponse<List<UserResponseDto>>
                {
                    Success = false,
                    Status = API_STATUS.ERROR,
                    Message = "取得使用者列表失敗"
                };
            }
        }

        public async Task<ApiResponse<UserResponseDto>> GetUserByIdAsync(int id, [CallerMemberName] string methodName = "")
        {
            try
            {
                await Task.Yield(); // 模擬非同步操作

                var user = Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    Log.Warning($"[{methodName}] 找不到ID為 {id} 的使用者");
                    return new ApiResponse<UserResponseDto>
                    {
                        Success = false,
                        Status = API_STATUS.NOT_FOUND,
                        Message = "找不到指定的使用者"
                    };
                }

                return new ApiResponse<UserResponseDto>
                {
                    Success = true,
                    Status = API_STATUS.SUCCESS,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[{methodName}] 取得使用者ID {id} 時發生錯誤 {ex.Message}");
                return new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Status = API_STATUS.ERROR,
                    Message = "取得使用者資料失敗"
                };
            }
        }

        public async Task<ApiResponse<UserResponseDto>> CreateUserAsync(string name, string email, [CallerMemberName] string methodName = "")
        {
            try
            {
                await Task.Yield(); // 模擬非同步操作

                var newUser = new UserResponseDto
                {
                    Id = Users.Count > 0 ? Users.Max(u => u.Id) + 1 : 1,
                    Name = name,
                    Email = email,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };

                Users.Add(newUser);

                return new ApiResponse<UserResponseDto>
                {
                    Success = true,
                    Status = API_STATUS.SUCCESS,
                    Data = newUser
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[{methodName}] 建立使用者時發生錯誤 {ex.Message}");
                return new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Status = API_STATUS.ERROR,
                    Message = "建立使用者失敗"
                };
            }
        }

        public async Task<ApiResponse<UserResponseDto>> UpdateUserAsync(int id, string name, string email, bool isActive, [CallerMemberName] string methodName = "")
        {
            try
            {
                await Task.Yield(); // 模擬非同步操作

                var user = Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    Log.Warning($"[{methodName}] 更新失敗，找不到ID為 {id} 的使用者");
                    return new ApiResponse<UserResponseDto>
                    {
                        Success = false,
                        Status = API_STATUS.NOT_FOUND,
                        Message = "找不到指定的使用者"
                    };
                }

                user.Name = name;
                user.Email = email;
                user.IsActive = isActive;

                return new ApiResponse<UserResponseDto>
                {
                    Success = true,
                    Status = API_STATUS.SUCCESS,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[{methodName}] 更新使用者ID {id} 時發生錯誤 {ex.Message}");
                return new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Status = API_STATUS.ERROR,
                    Message = "更新使用者資料失敗"
                };
            }
        }

        public async Task<ApiResponse<object>> DeleteUserAsync(int id, [CallerMemberName] string methodName = "")
        {
            try
            {
                await Task.Yield(); // 模擬非同步操作

                var user = Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    Log.Warning($"[{methodName}] 刪除失敗，找不到ID為 {id} 的使用者");
                    return new ApiResponse<object>
                    {
                        Success = false,
                        Status = API_STATUS.NOT_FOUND,
                        Message = "找不到指定的使用者"
                    };
                }

                Users.Remove(user);

                return new ApiResponse<object>
                {
                    Success = true,
                    Status = API_STATUS.SUCCESS,
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[{methodName}] 刪除使用者ID {id} 時發生錯誤 {ex.Message}");
                return new ApiResponse<object>
                {
                    Success = false,
                    Status = API_STATUS.ERROR,
                    Message = "刪除使用者失敗"
                };
            }
        }
    }
}
