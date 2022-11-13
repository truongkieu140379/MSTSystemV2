using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TutorSearchSystem.Filters
{
    public abstract class QueryStringParameter
    {
        const int maxPageSize = 50;
        [FromQuery(Name ="pageNumber")]
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        [FromQuery(Name = "pageSize")]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        
    }
}
