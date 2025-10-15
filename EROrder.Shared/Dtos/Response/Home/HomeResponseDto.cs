namespace EROrder.Shared.Dtos.Response.Station
{
    public class HomeResponseDto
    {
        public string Station { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
    }

    public class ERempResponseDto
    {
        public string EREmpCode { get; set; } = string.Empty;
        public string EREmpName { get; set; } = string.Empty;
        public string EREmpJobCode { get; set; } = string.Empty;
    }

    public class ERPatientResponseDto
    {
        public string ChartNo { get; set; } = string.Empty;// 病歷號
        public string PatientName { get; set; } = string.Empty;// 姓名
        public string Sex { get; set; } = string.Empty; // 性別
        public string Age { get; set; } = string.Empty;// 年齡
        public string BirthDate { get; set; } = string.Empty;// 生日
        public string DeptCode { get; set; } = string.Empty;// 科別
        public string DeptName { get; set; } = string.Empty;// 科別名稱
        public string InhospId { get; set; } = string.Empty;// INHOSPID
        public string NursingStation { get; set; } = string.Empty;// 護理站
        public string BedNo { get; set; } = string.Empty;// 床號
        public string TriageNo { get; set; } = string.Empty;// 檢傷序號
        public string TriageDept { get; set; } = string.Empty;// 檢傷科別
        public string TriageSession { get; set; } = string.Empty;// 檢傷午別 (MEM_NOON_NO)
        public string CheckInTime { get; set; } = string.Empty;// 入院時間
        public string StatusName { get; set; } = string.Empty;// 狀態
        public string VsCodeLast { get; set; } = string.Empty;// 主治醫師職編
        public string VsNameLast { get; set; } = string.Empty;// 主治醫師
        public string VsCodeFirst { get; set; } = string.Empty;// 預掛醫師職編
        public string VsNameFirst { get; set; } = string.Empty;// 預掛醫師
        public string VsCodeFollow { get; set; } = string.Empty;// 發落醫師職編
        public string VsNameFollow { get; set; } = string.Empty;// 發落醫師
        public string IcdCode { get; set; } = string.Empty;// ICD編碼
        public string IcdCodeEn { get; set; } = string.Empty;// ICD英文
        public string IcdCodeCh { get; set; } = string.Empty; // ICD中文
        public string BloodType { get; set; } = string.Empty;// 血型
        public string RhType { get; set; } = string.Empty;// 血型註記
    }
}
