using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public string PageName { get; set; }
        public PagingInfo() { }
        public PagingInfo(int totalItems, int page, int pageSize = 10)
        {
            int totalePages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPages = currentPage + 4;
            if (startPage <= 0)
            {
                endPages = endPages - (startPage - 1);
                startPage = 1;
            }
            if (endPages > totalePages)
            {
                endPages = totalePages;
                if (endPages > 10)
                {
                    startPage = endPages - 9;
                }
            }
            TotalItems = totalePages;
            CurrentPage = currentPage;
            ItemsPerPage = pageSize;
            TotalPages = totalePages;
            StartPage = startPage;
            EndPage = endPages;
        }
        //public int TotalPages =>
        //(int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
