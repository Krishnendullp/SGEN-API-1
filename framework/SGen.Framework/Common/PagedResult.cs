using Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGen.Framework.Common
{
    public class PagedResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

         //✅ Pagination fields
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }
        public int? TotalCount { get; set; }

        public static PagedResult<T> Ok(T data, string? message = null, int? pageNo = null, 
                                            int? pageSize = null, int? totalCount = null)
            => new PagedResult<T>
            {
                Success = true,
                Data = data,
                Message = message,
                PageNo = pageNo,PageSize = pageSize,TotalCount = totalCount
            };

        public static PagedResult<T> Fail(string message)
            => new PagedResult<T> { Success = false, Message = message };
    }
}
