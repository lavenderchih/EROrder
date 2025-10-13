using EROrder.Shared.Enums;

namespace EROrder.Shared.Dtos
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public API_STATUS Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
