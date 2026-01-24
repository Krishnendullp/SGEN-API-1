namespace Framework.Common
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        // ✅ Pagination fields
        //public int? PageNo { get; set; }
        //public int? PageSize { get; set; }
        //public int? TotalCount { get; set; }

        public static Result<T> Ok(T data, string? message = null /*int? pageNo = null, 
                                            int? pageSize = null, int? totalCount = null*/)
            => new Result<T> 
            { 
                Success = true, Data = data, Message = message, 
                //PageNo = pageNo,PageSize = pageSize,TotalCount = totalCount
            };

        public static Result<T> Fail(string message)
            => new Result<T> { Success = false, Message = message };
    }
}
