using System.Collections.Generic;

namespace WebApiSdk
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            IsSuccess = false;
            Errors = new List<string>();
            Result = default(T);
        }

        public bool IsSuccess { get; set; }

        public IList<string> Errors { get; set; }

        public T Result { get; set; }
    }
}