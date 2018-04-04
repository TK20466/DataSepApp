using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abstractions
{
    public class PagedSearchRequestBase
    {
        public PagedSearchRequestBase()
        {
            this.Page = 1;
            this.PageSize = 10;
            this.OrderBy = new List<string> { "+Id" };
        }

        public PagedSearchRequestBase(PagedSearchRequestBase other)
        {
            this.Page = other.Page;
            this.PageSize = other.PageSize;

            // TODO : should this be a item by item copy (clone) or is copy ok in this case
            this.OrderBy = other.OrderBy;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public List<string> OrderBy { get; set; }
    }
}
