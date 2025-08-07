using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Wrapper
{
    public class PagingResponse<T> : Response<T>
    {
        public int current_page { get; set; }
        public int page_size { get; set; }
        public int total_pages { get; set; }
        public int total_records { get; set; }

        public PagingResponse(T data, int currentPage, int pageSize, int totalRecords, string message = "OK", int code = 200)
            : base(true, code, message, data)   // ✅ Pass arguments to Response<T> constructor
        {
            current_page = currentPage;
            page_size = pageSize;
            total_records = totalRecords;
            total_pages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }
    }

}
