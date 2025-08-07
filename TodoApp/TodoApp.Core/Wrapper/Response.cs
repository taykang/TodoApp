using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Wrapper
{
    public class Response<T>
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public Response(bool success, int code, string message, T data)
        {
            this.success = success;
            this.code = code;
            this.message = message;
            this.data = data;
        }

        // ✅ Static helpers to make responses easier to create
        public static Response<T> SuccessResponse(T data, string message = "OK", int code = 200)
        {
            return new Response<T>(true, code, message, data);
        }

        public static Response<T> ErrorResponse(string message, int code = 400)
        {
            return new Response<T>(false, code, message, default);
        }
    }

}
