using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class PagedList
    {

        public PagedList( int count , int pageNumber,  int pageSize)
        {
            CurrentPage = pageNumber;
            TotalCount = count;
            PageSize = pageSize;
            Totalpages = (int) Math.Ceiling( count/ (double) pageSize);
        }
         public int CurrentPage { get; set; }
        public int Totalpages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}