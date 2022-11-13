using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Global
{
    public class Response<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }      
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public List<T> Data { get; set; }
    }
}
