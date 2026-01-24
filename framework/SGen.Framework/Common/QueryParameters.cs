using System;

namespace SGen.Framework.Common
{
    public class QueryParameters
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        // ✅ Multi-filter (Key = ColumnName, Value = SearchValue)
        public Dictionary<string, string>? Filters { get; set; }

        // ✅ Multi-sort (Key = ColumnName, Value = "asc"/"desc")
        public Dictionary<string, string>? Sorts { get; set; }
    }
}
