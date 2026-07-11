using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public sealed class ApiResponse<T>
    {
        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }
        public IReadOnlyList<string> Errors { get; init; } = [];
        public int StatusCode { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;


        public static ApiResponse<T> Success(T data, string message = "Success", int statusCode = 200) =>
            new()
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                StatusCode = statusCode
            };

        public static ApiResponse<T> Failure(
            string message,
            int statusCode,
            IReadOnlyList<string>? errors = null) =>
            new()
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode,
                Errors = errors ?? []
            };
        public static ApiResponse<T> Failure(Error error, int statusCode) =>
           new()
           {
               IsSuccess = false,
               Message = error.Message,
               StatusCode = statusCode,
               Errors = new List<string> { error.Code }
           };
         
    
        public static ApiResponse<T> NotFound(string message = "Resource not found") =>
            Failure(message, 404);

        public static ApiResponse<T> Unauthorized(string message = "Unauthorized") =>
            Failure(message, 401);

        public static ApiResponse<T> Forbidden(string message = "Forbidden") =>
            Failure(message, 403);

        public static ApiResponse<T> Conflict(string message) =>
            Failure(message, 409);

        public static ApiResponse<T> TooManyRequests(string message) =>
            Failure(message, 429);

        public static ApiResponse<T> Locked(string message) =>
            Failure(message, 423);
    }
}
