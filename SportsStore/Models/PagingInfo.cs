using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPages);
            }
        }

    }
}
