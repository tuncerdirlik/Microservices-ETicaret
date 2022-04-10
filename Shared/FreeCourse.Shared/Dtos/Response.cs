using FreeCourse.Shared.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FreeCourse.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; private set; }
        
        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        public List<string> Errors { get; set; }

        public static Response<T> Success(T data, ResponseStatusCodes statusCode)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = (int)statusCode,
                IsSuccessful = true
            };
        }

        public static Response<T> Success(ResponseStatusCodes statusCode)
        {
            return new Response<T>
            {
                Data = default(T),
                StatusCode = (int)statusCode,
                IsSuccessful = true
            };
        }

        public static Response<T> Fail(List<string> errors, ResponseStatusCodes statusCode)
        {
            return new Response<T>
            {
                Errors = errors,
                StatusCode = (int)statusCode,
                IsSuccessful = false
            };
        }

        public static Response<T> Fail(string error, ResponseStatusCodes statusCode)
        {
            return new Response<T>
            {
                Errors = new List<string> { error },
                StatusCode = (int)statusCode,
                IsSuccessful = false
            };
        }
    }
}
