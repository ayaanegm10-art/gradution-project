using Microsoft.AspNetCore.Identity.Data;

namespace try4.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        //public int UserId { get; set; } 
        public static ApiResponse<T> SuccessResponse(T? data, string? message = null)
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message };
        }

        public static ApiResponse<T> FailResponse(string message)
        {
            return new ApiResponse<T> { Success = false, Data = default, Message = message };
        }
    }
}