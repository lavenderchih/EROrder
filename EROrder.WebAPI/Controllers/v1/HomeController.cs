using EROrder.Core.Services;
using EROrder.Core.Services.Interfaces;
using EROrder.Shared.Dtos;
using EROrder.Shared.Dtos.Request.User;
using EROrder.Shared.Dtos.Response.Station;
using EROrder.Shared.Dtos.Response.User;
using EROrder.Shared.Enums;
using EROrder.Shared.Helpers;
using EROrder.WebAPI.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace EROrder.WebAPI.Controllers.v1
{
    [Route($"{MagicHelper.ApiRoutes.BaseApi}/{MagicHelper.ApiVersions.V1}/[controller]")]
    public class HomeController(IHomeService homeService) : BaseApiController
    {
        private readonly IHomeService _homeService = homeService;

        // 1.：護理站列表 ---
        // 網址: /api/v1/Home/NursingStations
        [HttpGet("NursingStations")]
        public async Task<ActionResult<ApiResponse<List<HomeResponseDto>>>> GetNursingStations()
        {
            var result = await _homeService.GetNursingStationsAsync();
            return Ok(result);
        }

        // 2.：員工列表 ---
        // 網址: /api/v1/Home/Employees
        [HttpGet("Employees")]
        public async Task<ActionResult<ApiResponse<List<ERempResponseDto>>>> GetEmployees()
        {
            var result = await _homeService.GetEREmpAsync();
            return Ok(result);
        }
    }
}
