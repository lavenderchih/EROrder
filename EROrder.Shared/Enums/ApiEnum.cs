namespace EROrder.Shared.Enums
{
    public enum API_STATUS
    {
        /// <summary>成功</summary>
        SUCCESS = 200,
        /// <summary>找不到資源</summary> 
        NOT_FOUND = 404,
        /// <summary>請求錯誤</summary>
        ERROR = 500,
        /// <summary>例外情形</summary>
        EXCEPTION = 9999,
    }
}
