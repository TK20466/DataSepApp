using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTypes
{
    public class WidgetSearchRequest : PagedSearchRequestBase
    {
        public string Name { get; set; }
    }
}
