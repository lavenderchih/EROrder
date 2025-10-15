using Dapper;
using EROrder.Core.Models;
using EROrder.Core.Services.Interfaces;
using EROrder.Shared.Dtos;
using EROrder.Shared.Dtos.Request.Home;
using EROrder.Shared.Dtos.Response.Station;
using EROrder.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EROrder.Core.Services
{
    public class HomeService:IHomeService
    {
        // 1. 宣告一個 DbContext 的私有欄位
        private readonly EROrderDbContext _context;

        // 2. 透過建構函式注入 DbContext
        //    DI 容器會自動幫您建立並傳入 DbContext 實例
        public HomeService(EROrderDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 撈取急診護理站列表
        /// </summary>
        /// <returns>護理站列表</returns>
        public async Task<ApiResponse<List<HomeResponseDto>>> GetNursingStationsAsync()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();

                // 將您的 SQL 語法放在這裡
                const string sql = @"
                    SELECT DISTINCT MEM_BED_CLASS AS Station, MEM_BED_CLASS || '區' AS StationName
                    FROM opd.rtxmem m
                    WHERE M.MEM_ENTER_DATE <= (SELECT ERD.FN_DATE2NUMBER(SYSDATE) FROM DUAL)
                      AND MEM_OUT_DATE = 0
                      AND TRIM(MEM_BED_CLASS) IS NOT NULL";

                // 使用 Dapper 執行查詢，並將結果自動對應到 StationResponseDto
                var stations = await connection.QueryAsync<HomeResponseDto>(sql);

                return new ApiResponse<List<HomeResponseDto>>
                {
                    Success = true,
                    Status = API_STATUS.SUCCESS,
                    Data = stations.ToList()
                };
            }
            catch (Exception ex)
            {
                // 記錄錯誤日誌
                Log.Error(ex, "[GetNursingStationsAsync] 取得急診護理站時發生錯誤: {ErrorMessage}", ex.Message);

                return new ApiResponse<List<HomeResponseDto>>
                {
                    Success = false,
                    Status = API_STATUS.ERROR,
                    Message = "取得護理站列表失敗"
                };
            }
        }

        public async Task<ApiResponse<List<ERempResponseDto>>> GetEREmpAsync()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();

                // 將您的 SQL 語法放在這裡
                const string sql = @"
                    SELECT E.EMP_CODE EREmpCode, E.EMP_NAME EREmpName, E.JOB_CODE EREmpJobCode
                      FROM KMUEMP E
                     WHERE E.DEPT_CODE = '1600' AND E.END_DATE IS NULL";

                // 使用 Dapper 執行查詢，並將結果自動對應到 ERempResponseDto
                var stations = await connection.QueryAsync<ERempResponseDto>(sql);

                return new ApiResponse<List<ERempResponseDto>>
                {
                    Success = true,
                    Status = API_STATUS.SUCCESS,
                    Data = stations.ToList()
                };
            }
            catch (Exception ex)
            {
                // 記錄錯誤日誌
                Log.Error(ex, "[GetNursingStationsAsync] 取得急診場域工作人員時發生錯誤: {ErrorMessage}", ex.Message);

                return new ApiResponse<List<ERempResponseDto>>
                {
                    Success = false,
                    Status = API_STATUS.ERROR,
                    Message = "取得急診場域工作人員列表失敗"
                };
            }
        }

        public async Task<ApiResponse<List<ERPatientResponseDto>>> GetERPatientAsync(ERPatientListRequestDto request)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();

                // 將您的 SQL 語法放在這裡
                const string sql = @"
SELECT V.病歷號,
       V.PTNAME 姓名,
       V.SEX 性別,
       V.AGE 年齡,
       V.BIRTHDATE 生日,
       V.DEPTCODE 科別,
       V.科別 科別名稱,
       V.INHOSPID,
       V.護理站,
       V.床號,
       V.檢傷序號,
       V.檢傷科別,
       V.MEM_NOON_NO 檢傷午別,
       TO_CHAR (V.CHECKINTIME, 'YYYY/MM/DD HH24:MI') 入院時間,
       V.STATUSNAME 狀態,
       V.LAST_VSCODE 主治醫師職編,
       V.LAST_VSNAME 主治醫師,
       V.FIRST_VSCODE 預掛醫師職編,
       V.FIRST_VSNAME 預掛醫師,
       V.FOLLOW_VSCODE 發落醫師職編,
       V.FOLLOW_VSNAME 發落醫師,
       V.ICDCODE AS ICD編碼,
       V.ICDCODE_EN AS ICD英文,
       V.ICDCODE_CH AS ICD中文,
       UIPD.PKG_BLDORDER.FN_GET_BLOODDATA (V.病歷號, 'BLOOD_TYPE') 血型,
       UIPD.PKG_BLDORDER.FN_GET_BLOODDATA (V.病歷號, 'RH_TYPE')
          血型註記
  FROM mcore.v_ioe_erlist v
 WHERE     v.mem_out_date = 0
       AND ( :inNS IS NULL OR TRIM (v.護理站) = :inNS)
       AND (      isvip
               || uipd.fn_chartspecial_readchart (v.病歷號,
                                                  :inLogin,
                                                  NULL,
                                                  'Y') IN ('YY', 'YP')
            OR isvip = 'N')";

                // 使用 Dapper 執行查詢，並將結果自動對應到 ERempResponseDto
                var stations = await connection.QueryAsync<ERPatientResponseDto>(sql);

                return new ApiResponse<List<ERPatientResponseDto>>
                {
                    Success = true,
                    Status = API_STATUS.SUCCESS,
                    Data = stations.ToList()
                };
            }
            catch (Exception ex)
            {
                // 記錄錯誤日誌
                Log.Error(ex, "[GetERPatientAsync] 取得急診病人時發生錯誤: {ErrorMessage}", ex.Message);

                return new ApiResponse<List<ERPatientResponseDto>>
                {
                    Success = false,
                    Status = API_STATUS.ERROR,
                    Message = "取得急診病人列表失敗"
                };
            }
        }
    }
}
